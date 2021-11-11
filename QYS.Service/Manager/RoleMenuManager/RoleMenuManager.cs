using QYS.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QYS.Service.Manager.RoleMenuManager.Dto;

namespace QYS.Service.Manager.RoleMenuManager
{
    public class RoleMenuManager : IRoleMenuManager
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly IMapper _mapper;

        public RoleMenuManager(IServiceScopeFactory serviceScopeFactory
            , IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// 根据菜单Code查找角色菜单
        /// </summary>
        /// <param name="menuCodes"></param>
        /// <returns></returns>
        public async Task<List<RoleMenuDto>> BatchFindByMenuCodeAsync(IEnumerable<string> menuCodes)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            var list = await context.RoleMenus.Where(p => menuCodes.Contains(p.MenuCode)).AsNoTracking().ToListAsync();

            return _mapper.Map<List<RoleMenuDto>>(list);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roleMenus"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBatchAsync(List<RoleMenuDto> roleMenus)
        {
            var roleMenusEntities = _mapper.Map<List<RoleMenu>>(roleMenus);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.RoleMenus.RemoveRange(roleMenusEntities);

            return await context.SaveChangesAsync() > 0;
        }
    }
}
