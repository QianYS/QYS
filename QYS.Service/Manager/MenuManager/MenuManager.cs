using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QYS.Entity;
using QYS.Service.Manager.MenuManager.Dto;

namespace QYS.Service.Manager.MenuManager
{
    public class MenuManager : IMenuManager
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly IMapper _mapper;

        public MenuManager(IServiceScopeFactory serviceScopeFactory
            , IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuDto>> GetAll()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            var list = await context.Menus.AsNoTracking().ToListAsync();

            return _mapper.Map<List<MenuDto>>(list);
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="menuCode"></param>
        /// <returns></returns>
        public async Task<Menu> FindByIdAsync(string menuCode)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            return await context.Menus.AsNoTracking().FirstOrDefaultAsync(p => p.Code == menuCode);
        }

        /// <summary>
        /// 根据名称查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Menu> FindByNameAsync(string name)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            return await context.Menus.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(MenuDto menu)
        { 
            var menuEntity = _mapper.Map<Menu>(menu);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            await context.Menus.AddAsync(menuEntity);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(MenuDto menu)
        {
            var menuEntity = _mapper.Map<Menu>(menu);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.Menus.Update(menuEntity);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(MenuDto menu)
        {
            var menuEntity = _mapper.Map<Menu>(menu);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.Menus.Remove(menuEntity);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        public async Task<bool> CreateBatchAsync(List<MenuDto> menus)
        {
            var menuEntities = _mapper.Map<List<Menu>>(menus);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            await context.Menus.AddRangeAsync(menuEntities);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBatchAsync(List<MenuDto> menus)
        {
            var menuEntities = _mapper.Map<List<Menu>>(menus);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.Menus.UpdateRange(menuEntities);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBatchAsync(List<MenuDto> menus)
        {
            var menuEntities = _mapper.Map<List<Menu>>(menus);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.Menus.RemoveRange(menuEntities);

            return await context.SaveChangesAsync() > 0;
        }

    }
}
