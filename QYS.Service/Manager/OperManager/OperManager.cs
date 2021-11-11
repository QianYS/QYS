using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QYS.Entity;
using QYS.Service.Manager.OperManager.Dto;

namespace QYS.Service.Manager.OperManager
{
    public class OperManager : IOperManager
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly IMapper _mapper;

        public OperManager(IServiceScopeFactory serviceScopeFactory
            , IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取所有操作
        /// </summary>
        /// <returns></returns>
        public async Task<List<OperDto>> GetAll()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            var list = await context.Opers.AsNoTracking().ToListAsync();

            return _mapper.Map<List<OperDto>>(list);
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public async Task<Oper> FindByIdAsync(string operCode)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            return await context.Opers.AsNoTracking().FirstOrDefaultAsync(p => p.Code == operCode);
        }

        /// <summary>
        /// 根据名称查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Oper> FindByNameAsync(string name)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            return await context.Opers.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(OperDto menu)
        { 
            var roleEntity = _mapper.Map<Oper>(menu);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            await context.Opers.AddAsync(roleEntity);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(OperDto menu)
        {
            var roleEntity = _mapper.Map<Oper>(menu);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.Opers.Update(roleEntity);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(OperDto menu)
        {
            var roleEntity = _mapper.Map<Oper>(menu);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.Opers.Remove(roleEntity);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task<bool> CreateBatchAsync(List<OperDto> roles)
        {
            var roleEntities = _mapper.Map<List<Oper>>(roles);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            await context.Opers.AddRangeAsync(roleEntities);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBatchAsync(List<OperDto> roles)
        {
            var roleEntities = _mapper.Map<List<Oper>>(roles);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.Opers.UpdateRange(roleEntities);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBatchAsync(List<OperDto> roles)
        {
            var roleEntities = _mapper.Map<List<Oper>>(roles);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.Opers.RemoveRange(roleEntities);

            return await context.SaveChangesAsync() > 0;
        }

    }
}
