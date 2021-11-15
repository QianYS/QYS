using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QYS.Service.Service.TaskSvr.Dto;

namespace QYS.Service.Service.TaskSvr
{
    public interface ITaskService
    {
        /// <summary>
        /// 获取所有系统中的任务
        /// </summary>
        /// <returns></returns>
        Task<List<TaskDto>> GetAllJobs();

        /// <summary>
        /// 手动处理
        /// </summary>
        /// <param name="jobGroup"></param>
        /// <param name="jobName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        Task<string> ManualTask(string jobGroup, string jobName, EnumJobActions action);
    }
}
