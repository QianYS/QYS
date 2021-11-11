using QYS.Entity;
using QYS.Service.Manager.RoleManager.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace QYS.Service.Manager.RoleManager
{
    public class RoleManager : IRoleManager
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly IMapper _mapper;

        public RoleManager(IServiceScopeFactory serviceScopeFactory
            , IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<Role> FindByIdAsync(int roleId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            return await context.Roles.FirstOrDefaultAsync(p => p.Id == roleId);
        }

        /// <summary>
        /// 根据名称查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Role> FindByNameAsync(string name)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            return await context.Roles.FirstOrDefaultAsync(p => p.Name == name);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(RoleDto role)
        { 
            var roleEntity = _mapper.Map<Role>(role);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            await context.Roles.AddAsync(roleEntity);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(RoleDto role)
        {
            var roleEntity = _mapper.Map<Role>(role);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.Roles.Update(roleEntity);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(RoleDto role)
        {
            var roleEntity = _mapper.Map<Role>(role);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.Roles.Remove(roleEntity);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task<bool> CreateBatchAsync(List<RoleDto> roles)
        {
            var roleEntities = _mapper.Map<List<Role>>(roles);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            await context.Roles.AddRangeAsync(roleEntities);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBatchAsync(List<RoleDto> roles)
        {
            var roleEntities = _mapper.Map<List<Role>>(roles);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.Roles.UpdateRange(roleEntities);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBatchAsync(List<RoleDto> roles)
        {
            var roleEntities = _mapper.Map<List<Role>>(roles);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.Roles.RemoveRange(roleEntities);

            return await context.SaveChangesAsync() > 0;
        }

    }
}
