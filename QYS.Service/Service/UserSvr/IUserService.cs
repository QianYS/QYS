using System.Threading.Tasks;
using QYS.Service.Service.UserSvr.Model;

namespace QYS.Service.Service.UserSvr
{
    /// <summary>
    /// IUserService 接口
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<TokenResult> RegisterAsync(string username, string password, string address);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<TokenResult> LoginAsync(string username, string password);

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<TokenResult> RefreshTokenAsync(string token, string refreshToken);
    }
}
