using Quartz;

namespace QYS_Project.Responses.MngApi.Task
{
    /// <summary>
    /// 定时任务列表
    /// </summary>
    public class TaskListResponse
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
        public string JobStatusStr { get; set; }
        
    }
}
