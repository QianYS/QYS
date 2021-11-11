using QYS.Service.Manager.MenuManager.Dto;
using QYS.Service.Manager.OperManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QYS.Service.Manager.MenuManager;
using QYS.Service.Manager.OperManager;
using QYS.Service.Manager.RoleMenuManager;
using QYS.Service.Manager.RoleOperManager;
using QYS.Service.Service.SystemSvr.Dto;
using QYS.Service.Tool;

namespace QYS.Service.Service.SystemSvr
{
    public class SystemService : ISystemService
    {
        private readonly IMapper _mapper;

        private readonly IMenuManager _menuManager;

        private readonly IOperManager _operManager;

        private readonly IRoleMenuManager _roleMenuManager;

        private readonly IRoleOperManager _roleOperManager;

        public SystemService(IMapper mapper
            , IMenuManager menuManager
            , IOperManager operManager
            , IRoleMenuManager roleMenuManager
            , IRoleOperManager roleOperManager)
        {
            _mapper = mapper;
            _menuManager = menuManager;
            _operManager = operManager;
            _roleMenuManager = roleMenuManager;
            _roleOperManager = roleOperManager;
        }

        /// <summary>
        /// 解析初始化数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<Tuple<List<MenuDto>, List<OperDto>>> Structure(IEnumerable<InitSystemDto> data)
        {
            var menuList = new List<MenuDto>();
            var operList = new List<OperDto>();
            foreach (var item in data)
            {
                var (menus, opers) = GetMapper(item);
                if (menus.Count > 0)
                    menuList.AddRange(menus);

                if (opers.Count > 0)
                    operList.AddRange(opers);
            }
            return Task.FromResult(Tuple.Create(menuList, operList));
        }

        private Tuple<List<MenuDto>, List<OperDto>> GetMapper(InitSystemDto dto, string parentCode = "")
        {
            var menuList=new List<MenuDto>();
            var operList = new List<OperDto>();

            var menu = _mapper.Map<MenuDto>(dto);
            menu.ParentCode = parentCode;

            if (dto.MenuOpers != null && dto.MenuOpers.Count > 0)
            {
                var opers = _mapper.Map<List<OperDto>>(dto.MenuOpers);

                opers.ForEach((x) =>
                {
                    x.Code = EncryptionHelper.Md5Encrypt32(string.Concat(menu.Code, x.Method, x.Action));
                    x.MenuCode = menu.Code;
                });

                operList.AddRange(opers);
            }

            if (dto.Children == null || dto.Children.Count <= 0)
                menuList.Add(menu);
            else
            {
                menuList.Add(menu);
                foreach (var child in dto.Children)
                {
                    var (menus, opers) = GetMapper(child, menu.Code);
                    if (menus.Count > 0)
                        menuList.AddRange(menus);

                    if (opers.Count > 0)
                        operList.AddRange(opers);
                }
            }

            return Tuple.Create(menuList, operList);

        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        public async Task UpdateMenu(IEnumerable<MenuDto> menus)
        {
            var existMenus = await _menuManager.GetAll();
            var existDic = existMenus.ToDictionary(x => x.Code, y => y);

            var afferentDic = menus.ToDictionary(x => x.Code, y => y);

            var updateMenus = new List<MenuDto>();

            // 需删除的菜单Code
            var deleteMenus = new List<MenuDto>();
            foreach (var (key, value) in existDic)
            {
                if (!afferentDic.ContainsKey(key))
                    deleteMenus.Add(value);
            }

            // 需创建的菜单Code
            var createMenus = new List<MenuDto>();
            foreach (var (key, value) in afferentDic)
            {
                if (!existDic.ContainsKey(key))
                    createMenus.Add(value);
                else
                {
                    var data = existDic[key];
                    data.Name = value.Name;
                    data.Icon = value.Icon;
                    data.LastUpdate = DateTime.Now;

                    updateMenus.Add(data);
                }
            }

            if (createMenus.Count > 0)
                await _menuManager.CreateBatchAsync(createMenus);

            if (updateMenus.Count > 0)
                await _menuManager.UpdateBatchAsync(updateMenus);

            if (deleteMenus.Count > 0)
            {
                await _menuManager.DeleteBatchAsync(deleteMenus);

                var deleteKeys = deleteMenus.Select(p => p.Code).ToArray();
                var roleMenus = await _roleMenuManager.BatchFindByMenuCodeAsync(deleteKeys);

                if(roleMenus.Count>0)
                    await _roleMenuManager.DeleteBatchAsync(roleMenus);
            }
                
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="opers"></param>
        /// <returns></returns>
        public async Task UpdateOper(IEnumerable<OperDto> opers)
        {
            var existOpers = await _operManager.GetAll();
            var existDic = existOpers.ToDictionary(x => x.Code, y => y);

            var afferentDic = opers.ToDictionary(x => x.Code, y => y);

            var updateOpers = new List<OperDto>();

            // 需删除的菜单Code
            var deleteOpers = new List<OperDto>();
            foreach (var (key, value) in existDic)
            {
                if (!afferentDic.ContainsKey(key))
                    deleteOpers.Add(value);
            }

            // 需创建的菜单Code
            var createOpers = new List<OperDto>();
            foreach (var (key, value) in afferentDic)
            {
                if (!existDic.ContainsKey(key))
                    createOpers.Add(value);
                else
                {
                    var data = existDic[key];
                    data.Name = value.Name;
                    data.LastUpdate = DateTime.Now;

                    updateOpers.Add(data);
                }
            }

            if (createOpers.Count > 0)
                await _operManager.CreateBatchAsync(createOpers);

            if (updateOpers.Count > 0)
                await _operManager.UpdateBatchAsync(updateOpers);

            if (deleteOpers.Count > 0)
            {
                await _operManager.DeleteBatchAsync(deleteOpers);

                var deleteKeys = deleteOpers.Select(p => p.Code).ToArray();
                var roleOpers = await _roleOperManager.BatchFindByOperCodeAsync(deleteKeys);

                if (roleOpers.Count > 0)
                    await _roleOperManager.DeleteBatchAsync(roleOpers);

            }
        }
    }
}
