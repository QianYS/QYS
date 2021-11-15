using Quartz;
using QYS.Service.Service.TaskSvr.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;

namespace QYS.Service.Service.TaskSvr
{
    public class TaskService : ITaskService
    {
        private readonly ISchedulerFactory _schedulerFactory;

        public TaskService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        public async Task<List<TaskDto>> GetAllJobs()
        {
            var returnDto = new List<TaskDto>();
            var scheduler = await _schedulerFactory.GetScheduler();
            var groups = await scheduler.GetJobGroupNames();
            foreach (var groupName in groups)
            {
                var jobList = await scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName));

                foreach (var jobKey in jobList)
                {
                    var state = await scheduler.GetTriggerState(new TriggerKey(jobKey.Name));

                    returnDto.Add(new TaskDto()
                        {JobGroup = jobKey.Group, JobName = jobKey.Name, JobStatus = state});
                }

                returnDto.AddRange(jobList.Select(jobKey => new TaskDto()
                    { JobGroup = jobKey.Group, JobName = jobKey.Name}));
            }

            return returnDto;
        }

        /// <summary>
        /// 手动处理
        /// </summary>
        /// <param name="jobGroup"></param>
        /// <param name="jobName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task<string> ManualTask(string jobGroup, string jobName, EnumJobActions action)
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            var jobKeys = scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(jobGroup)).Result.ToList();
            if (!jobKeys.Any())
                return $"未找到分组[{jobGroup}]";

            var jobKey = jobKeys.FirstOrDefault(s => scheduler.GetTriggersOfJob(s).Result.Any(x => (x as CronTriggerImpl)?.Name == jobName));
            if (jobKey == null)
                return $"未找到任务[{jobName}]";

            var triggers = await scheduler.GetTriggersOfJob(jobKey);
            var trigger = triggers?.Where(x => (x as CronTriggerImpl)?.Name == jobGroup).FirstOrDefault();
            if (trigger == null)
                return $"未找到触发器[{jobGroup}{jobName}]";

            switch (action)
            {
                case EnumJobActions.Suspend:
                    await scheduler.PauseTrigger(trigger.Key);
                    break;
                case EnumJobActions.Start:
                    await scheduler.ResumeTrigger(trigger.Key);
                    break;
                case EnumJobActions.Execute:
                    await scheduler.TriggerJob(jobKey);
                    break;
                case EnumJobActions.Stop:
                    await scheduler.Shutdown();
                    break;
                default:
                    return "未找到对应的操作";
            }

            return string.Empty;
        }
    }
}
