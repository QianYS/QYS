using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QYS.Service.Service.TaskSvr.Dto;

namespace QYS_Project.Requests.MngApi.Task
{
    /// <summary>
    /// 后台任务查询
    /// </summary>
    public class TaskQueryRequest
    {
        /// <summary>
        /// 任务组
        /// </summary>
        public string JobGroup { get; set; }

        /// <summary>
        /// 任务名
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 页码信息
        /// </summary>
        public PageInfo PageInfo { get; set; }
    }
}
