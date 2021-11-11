using System.Collections.Generic;
using System.Threading.Tasks;
using QYS.Service.Manager.RoleOperManager.Dto;

namespace QYS.Service.Manager.RoleOperManager
{
    public interface IRoleOperManager
    {
        /// <summary>
        /// 根据操作Code查找角色操作
        /// </summary>
        /// <param name="operCodes"></param>
        /// <returns></returns>
        Task<List<RoleOperDto>> BatchFindByOperCodeAsync(IEnumerable<string> operCodes);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roleOpers"></param>
        /// <returns></returns>
        Task<bool> DeleteBatchAsync(List<RoleOperDto> roleOpers);
    }
}
