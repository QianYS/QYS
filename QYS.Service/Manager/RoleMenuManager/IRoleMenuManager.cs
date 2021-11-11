using System.Collections.Generic;
using System.Threading.Tasks;
using QYS.Service.Manager.RoleMenuManager.Dto;

namespace QYS.Service.Manager.RoleMenuManager
{
    public interface IRoleMenuManager
    {
        /// <summary>
        /// 根据菜单Code查找角色菜单
        /// </summary>
        /// <param name="menuCodes"></param>
        /// <returns></returns>
        Task<List<RoleMenuDto>> BatchFindByMenuCodeAsync(IEnumerable<string> menuCodes);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roleMenus"></param>
        /// <returns></returns>
        Task<bool> DeleteBatchAsync(List<RoleMenuDto> roleMenus);
    }
}
