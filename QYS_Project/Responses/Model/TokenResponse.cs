using System.Text.Json.Serialization;

namespace QYS_Project.Responses.Model
{
    /// <summary>
    /// 注册 登录 成功后返回 token
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// access_token
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// token类型
        /// </summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// refresh_token
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
