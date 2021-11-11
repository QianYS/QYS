using System.Collections.Generic;
using System.Threading.Tasks;
using QYS.Entity;
using QYS.Service.Manager.MenuManager.Dto;

namespace QYS.Service.Manager.MenuManager
{
    public interface IMenuManager
    {
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        Task<List<MenuDto>> GetAll();

        /// <summary>
        /// 根据Id查找菜单
        /// </summary>
        /// <param name="menuCode"></param>
        /// <returns></returns>
        Task<Menu> FindByIdAsync(string menuCode);

        /// <summary>
        /// 根据名称查找菜单
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Menu> FindByNameAsync(string name);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(MenuDto menu);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(MenuDto menu);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(MenuDto menu);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        Task<bool> CreateBatchAsync(List<MenuDto> menus);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        Task<bool> UpdateBatchAsync(List<MenuDto> menus);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        Task<bool> DeleteBatchAsync(List<MenuDto> menus);
    }
}
