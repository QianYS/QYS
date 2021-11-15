using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QYS.Service.Service.TaskSvr.Dto;

namespace QYS_Project.Requests.MngApi.Task
{
    /// <summary>
    /// 手动处理请求
    /// </summary>
    public class ManualTaskRequest
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
        /// 任务操作
        /// </summary>
        public EnumJobActions JobAction { get; set; }
    }
}
