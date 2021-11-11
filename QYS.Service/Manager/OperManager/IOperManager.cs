using System.Collections.Generic;
using System.Threading.Tasks;
using QYS.Entity;
using QYS.Service.Manager.OperManager.Dto;

namespace QYS.Service.Manager.OperManager
{
    public interface IOperManager
    {
        /// <summary>
        /// 获取所有操作
        /// </summary>
        /// <returns></returns>
        Task<List<OperDto>> GetAll();

        /// <summary>
        /// 根据主键查找操作
        /// </summary>
        /// <param name="operCode"></param>
        /// <returns></returns>
        Task<Oper> FindByIdAsync(string operCode);

        /// <summary>
        /// 根据名称查找操作
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Oper> FindByNameAsync(string name);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(OperDto oper);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(OperDto oper);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(OperDto oper);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="opers"></param>
        /// <returns></returns>
        Task<bool> CreateBatchAsync(List<OperDto> opers);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="opers"></param>
        /// <returns></returns>
        Task<bool> UpdateBatchAsync(List<OperDto> opers);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="opers"></param>
        /// <returns></returns>
        Task<bool> DeleteBatchAsync(List<OperDto> opers);
    }
}
