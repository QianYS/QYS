using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QYS.Entity;
using QYS.Service.Common;
using QYS.Service.Manager.RefreshTokenManager;
using QYS.Service.Manager.UserManager;
using QYS.Service.Service.UserSvr.Model;

namespace QYS.Service.Service.UserSvr
{
    // UserService 实现
    public class UserService : IUserService
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IUserManager _userManager;

        private readonly IRefreshTokenManager _refreshTokenManager;

        public UserService(IOptionsMonitor<JwtSettings> jwtSettings
            , IUserManager userManager
            , IRefreshTokenManager refreshTokenManager)
        {
            _jwtSettings = jwtSettings.CurrentValue;
            _userManager = userManager;
            _refreshTokenManager = refreshTokenManager;
        }

        public async Task<TokenResult> RegisterAsync(string username, string password, string address)
        {
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null)
                return new TokenResult() {Errors = "用户已存在!"};
            var newUser = new User() { LoginName = username, Address = address, Gender = EnumGenders.Male, CreateTime = DateTime.Now, LastLoginTime = DateTime.Now, LastUpdate = DateTime.Now};
            var isCreated = await _userManager.CreateAsync(newUser, password);
            if (!isCreated)
                return new TokenResult() {Errors = "注册失败!"};
            return await GenerateJwtToken(newUser);
        }

        public async Task<TokenResult> LoginAsync(string username, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser == null)
                return new TokenResult() {Errors = "用户不存在!"};
            var isCorrect = _userManager.CheckPasswordAsync(existingUser, password);
            if (!isCorrect)
                return new TokenResult() {Errors = "用户名或密码错误!"};
            return await GenerateJwtToken(existingUser);
        }

        public async Task<TokenResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var claimsPrincipal = GetClaimsPrincipalByToken(token);
            if (claimsPrincipal == null)
                return new TokenResult() {Errors = "无效的token!"};

            var expiryDateUnix =
                long.Parse(claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = UnixTimeStampToDateTime(expiryDateUnix);
            if (expiryDateTimeUtc > DateTime.UtcNow)
                return new TokenResult(){Errors = "token未过期!" };

            var jti = claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _refreshTokenManager.GetByRefreshToken(refreshToken);
            if (storedRefreshToken == null)
                return new TokenResult() {Errors = "无效的refresh_token!"};

            if (storedRefreshToken.ExpiryTime < DateTime.UtcNow)
                return new TokenResult() {Errors = "refresh_token已过期!"};

            if (storedRefreshToken.Invalidated)
                return new TokenResult() {Errors = "refresh_token已失效!"};

            if (storedRefreshToken.Used)
                return new TokenResult() {Errors = "refresh_token已使用!"};

            if (storedRefreshToken.JwtId != jti)
                return new TokenResult() {Errors = "refresh_token与此token不匹配!"};

            storedRefreshToken.Used = true;

            await _refreshTokenManager.Update(storedRefreshToken);

            var dbUser = await _userManager.FindByIdAsync(storedRefreshToken.UserId);
            return await GenerateJwtToken(dbUser);
        }

        private async Task<TokenResult> GenerateJwtToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(ConstValue.UserId, user.Id.ToString()),
                    new Claim(ConstValue.LoginName, user.LoginName),
                    new Claim(ConstValue.Name, user.Name)
                }),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpiresIn),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtTokenHandler.CreateToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(securityToken);

            var refreshToken = new RefreshToken()
            {
                JwtId = securityToken.Id,
                UserId = user.Id,
                CreateTime = DateTime.UtcNow,
                ExpiryTime = DateTime.UtcNow.AddMonths(6),
                Token = GenerateRandomNumber()
            };

            if (await _refreshTokenManager.CreateAsync(refreshToken))
                return new TokenResult()
                {
                    AccessToken = token,
                    TokenType = "Bearer",
                    RefreshToken = refreshToken.Token,
                    ExpiresIn = (int)_jwtSettings.ExpiresIn.TotalSeconds,
                };
            return new TokenResult() {Errors = "保存refreshToken错误"};
        }

        private string GenerateRandomNumber(int len = 32)
        {
            var randomNumber = new byte[len];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetClaimsPrincipalByToken(string token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecurityKey)),
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false // 不验证过期时间！！！
                };

                var jwtTokenHandler = new JwtSecurityTokenHandler();

                var claimsPrincipal =
                    jwtTokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                var validatedSecurityAlgorithm = validatedToken is JwtSecurityToken jwtSecurityToken
                                                 && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                                     StringComparison.InvariantCultureIgnoreCase);

                return validatedSecurityAlgorithm ? claimsPrincipal : null;
            }
            catch
            {
                return null;
            }
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }

    }
}
