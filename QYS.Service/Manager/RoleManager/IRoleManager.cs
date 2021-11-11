using System.Collections.Generic;
using System.Threading.Tasks;
using QYS.Entity;
using QYS.Service.Manager.RoleManager.Dto;

namespace QYS.Service.Manager.RoleManager
{
    public interface IRoleManager
    {
        /// <summary>
        /// 根据Id查找角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Role> FindByIdAsync(int userId);

        /// <summary>
        /// 根据名称查找角色
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Role> FindByNameAsync(string name);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(RoleDto role);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(RoleDto role);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(RoleDto role);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        Task<bool> CreateBatchAsync(List<RoleDto> roles);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        Task<bool> UpdateBatchAsync(List<RoleDto> roles);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        Task<bool> DeleteBatchAsync(List<RoleDto> roles);
    }
}
