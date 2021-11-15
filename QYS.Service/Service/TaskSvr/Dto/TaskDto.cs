using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Quartz;

namespace QYS.Service.Service.TaskSvr.Dto
{
    public class TaskDto
    {
        /// <summary>
        /// 任务组
        /// </summary>
        public string JobGroup { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public TriggerState JobStatus { get; set; }

        /// <summary>
        /// 任务状态（展示）
        /// </summary>
        public string JobStatusStr
        {
            get
            {
                switch (JobStatus)
                {
                    case TriggerState.Blocked:
                        return "阻塞";
                    case TriggerState.Complete:
                        return "完成";
                    case TriggerState.Error:
                        return "错误";
                    case TriggerState.None:
                        return "不存在";
                    case TriggerState.Normal:
                        return "正常";
                    case TriggerState.Paused:
                        return "暂停";
                    default:
                        return "未知状态";
                }
            }
        }
    }

}
