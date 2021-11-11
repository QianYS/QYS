using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QYS.Entity;
using QYS.Service.Manager.RoleOperManager.Dto;

namespace QYS.Service.Manager.RoleOperManager
{
    public class RoleOperManager : IRoleOperManager
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly IMapper _mapper;

        public RoleOperManager(IServiceScopeFactory serviceScopeFactory
            , IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// 根据操作Code查找角色操作
        /// </summary>
        /// <param name="operCodes"></param>
        /// <returns></returns>
        public async Task<List<RoleOperDto>> BatchFindByOperCodeAsync(IEnumerable<string> operCodes)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            var list = await context.RoleOpers.Where(p => operCodes.Contains(p.OperCode)).AsNoTracking().ToListAsync();

            return _mapper.Map<List<RoleOperDto>>(list);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roleOpers"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBatchAsync(List<RoleOperDto> roleOpers)
        {
            var roleOpersEntities = _mapper.Map<List<RoleOper>>(roleOpers);

            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QYSDbContext>();

            context.RoleOpers.RemoveRange(roleOpersEntities);

            return await context.SaveChangesAsync() > 0;
        }
    }
}
