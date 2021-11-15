using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using QYS.Service.Manager.MenuManager;
using QYS.Service.Manager.OperManager;
using QYS.Service.Manager.RefreshTokenManager;
using QYS.Service.Manager.RoleManager;
using QYS.Service.Manager.RoleMenuManager;
using QYS.Service.Manager.RoleOperManager;
using QYS.Service.Manager.UserManager;
using QYS.Service.Service.SystemSvr;
using QYS.Service.Service.TaskSvr;
using QYS.Service.Service.UserSvr;

namespace QYS.Service
{
    /// <summary>
    /// 注册服务
    /// </summary>
    public static class Installer
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            //管理服务
            services.AddScoped<IUserManager, UserManager>();

            services.AddScoped<IRoleManager, RoleManager>();

            services.AddScoped<IMenuManager, MenuManager>();

            services.AddScoped<IOperManager, OperManager>();

            services.AddScoped<IRoleMenuManager, RoleMenuManager>();

            services.AddScoped<IRoleOperManager, RoleOperManager>();

            services.AddScoped<IRefreshTokenManager, RefreshTokenManager>();


            //服务
            services.AddScoped<ISystemService, SystemService>();

            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IUserService, UserService>();

            

            return services;
        }
    }
}
