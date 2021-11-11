using Quartz;
using Quartz.Impl;

namespace QYS.BackGroundTask
{
    /// <summary>
    /// 模块管理器
    /// </summary>
    public class SchedualHelper
    {
        /// <summary>
        /// 计划任务
        /// </summary>
        public static IScheduler Scheduler { get; private set; }

        /// <summary>
        /// 加载所有模块
        /// </summary>
        static SchedualHelper()
        {
            var sf = new StdSchedulerFactory();
            Scheduler = sf.GetScheduler().Result;
            Scheduler.Start();
        }

        /// <summary>
        /// 添加计划任务
        /// </summary>
        /// <param name="jobDetail"></param>
        /// <param name="trigger"></param>
        private static void ScheduleJob(IJobDetail jobDetail, ITrigger trigger)
        {
            Scheduler.ScheduleJob(jobDetail, trigger);
        }

        /// <summary>
        /// 添加计划任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="cron"></param>
        public static void SchedualJob<T>(string name, string cron) where T : IJob
        {
            var job = JobBuilder.Create<T>().Build();

            //创建触发器
            var trigger = TriggerBuilder.Create()
                .WithIdentity(name)
                .StartNow()
                .WithCronSchedule(cron)
                .Build();

            //加入作业调度池中
            ScheduleJob(job, trigger);
        }

    }
}
