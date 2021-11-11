using System.Threading.Tasks;
using QYS.Entity;

namespace QYS.Service.Manager.UserManager
{
    public interface IUserManager
    {
        /// <summary>
        /// 根据userId查找用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> FindByIdAsync(int userId);

        /// <summary>
        /// 根据姓名查找用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<User> FindByNameAsync(string name);

        /// <summary>
        /// 检查密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool CheckPasswordAsync(User user, string password);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(User user, string passWord);
    }
}
