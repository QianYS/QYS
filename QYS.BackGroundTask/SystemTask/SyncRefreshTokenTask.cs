using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Quartz;
using QYS.Service;
using QYS.Service.Manager.UserManager;

namespace QYS.BackGroundTask.SystemTask
{
    public class SyncRefreshTokenTask : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using (var serviceScope = Machine.ServiceProvider.CreateScope())//单例不需要创建直接能拿去
            {
                var dbContext = serviceScope.ServiceProvider.GetService<QYSDbContext>();
                var users = await dbContext.Users.ToListAsync();
                await Console.Out.WriteLineAsync("查询到的数据1:" + JsonConvert.SerializeObject(users));

                var userManager= serviceScope.ServiceProvider.GetService<IUserManager>();
                var user = userManager.FindByIdAsync(1);
                await Console.Out.WriteLineAsync("查询到的数据2:" + JsonConvert.SerializeObject(user));
            }

            await Console.Out.WriteLineAsync($"{DateTime.Now:HH:mm:ss}--Hello World!");
        }
    }
}
