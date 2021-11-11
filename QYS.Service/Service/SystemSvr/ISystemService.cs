using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QYS.Service.Manager.MenuManager.Dto;
using QYS.Service.Manager.OperManager.Dto;
using QYS.Service.Service.SystemSvr.Dto;

namespace QYS.Service.Service.SystemSvr
{
    public interface ISystemService
    {
        /// <summary>
        /// 解析传入
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<Tuple<List<MenuDto>, List<OperDto>>> Structure(IEnumerable<InitSystemDto> data);

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        Task UpdateMenu(IEnumerable<MenuDto> menus);

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="opers"></param>
        /// <returns></returns>
        Task UpdateOper(IEnumerable<OperDto> opers);

    }
}
