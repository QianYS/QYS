using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QYS.Entity;

namespace QYS.Service.Manager.RefreshTokenManager
{
    public class RefreshTokenManager : IRefreshTokenManager
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RefreshTokenManager(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(RefreshToken refreshToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            await context.RefreshTokens.AddAsync(refreshToken);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<RefreshToken> GetByRefreshToken(string refreshToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            return await context.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(p => p.Token == refreshToken);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<bool> Update(RefreshToken refreshToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.RefreshTokens.Update(refreshToken);

            return await context.SaveChangesAsync() > 0;
        }
    }
}
