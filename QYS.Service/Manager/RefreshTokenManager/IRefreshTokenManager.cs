using System.Threading.Tasks;
using QYS.Entity;

namespace QYS.Service.Manager.RefreshTokenManager
{
    public interface IRefreshTokenManager
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(RefreshToken refreshToken);

        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<RefreshToken> GetByRefreshToken(string refreshToken);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<bool> Update(RefreshToken refreshToken);
    }
}
