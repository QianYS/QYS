using System;
using QYS.BackGroundTask.SystemTask;

namespace QYS.BackGroundTask
{
    public static class Machine
    {
        public static IServiceProvider ServiceProvider;

        public static void Start()
        {
            SchedualHelper.SchedualJob<SyncRefreshTokenTask>("同步RefreshToken","System", "0/10 * * * * ? ");

        }
    }
}
