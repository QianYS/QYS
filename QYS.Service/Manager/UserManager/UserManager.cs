using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QYS.Entity;
using QYS.Service.Tool;

namespace QYS.Service.Manager.UserManager
{
    public class UserManager: IUserManager
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UserManager(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// 验证码检查
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPasswordAsync(User user, string password)
        {
            var passwordHash = EncryptionHelper.Md5Encrypt32(password);

            return passwordHash== user.PassWord;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(User user, string password)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            var passwordHash = EncryptionHelper.Md5Encrypt32(password);

            user.PassWord = passwordHash;

            await context.Users.AddAsync(user);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<User> FindByNameAsync(string name)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            return await context.Users.FirstOrDefaultAsync(p => p.LoginName == name);
        }

        /// <summary>
        /// 根据用户Id获取用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User> FindByIdAsync(int userId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            return await context.Users.FirstOrDefaultAsync(p => p.Id == userId);
        }
    }
}
