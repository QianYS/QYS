using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QYS.Service;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QYS.Service.Common;
using QYS.Service.Manager.RefreshTokenManager;
using QYS.Service.Manager.UserManager;
using QYS_Project.Helper;
using QYS.BackGroundTask;
using QYS.Service.Manager.MenuManager;
using QYS.Service.Manager.OperManager;
using QYS.Service.Manager.RoleManager;
using QYS.Service.Manager.RoleMenuManager;
using QYS.Service.Manager.RoleOperManager;
using QYS.Service.Service.SystemSvr;
using QYS.Service.Service.UserSvr;

namespace QYS_Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddOptions();

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            services.AddDbContextPool<QYSDbContext>(options =>
            {
                var connStr = Configuration.GetConnectionString("DefaultConnection");
                options.UseMySQL(connStr);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtSettings:SecurityKey"])),
                ClockSkew = TimeSpan.Zero,
            };
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => { options.TokenValidationParameters = tokenValidationParameters; });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "QYS.Api",
                    Version = "v1",
                    Description = "Sample.Api Swagger Doc"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Input the JWT like: Bearer {your token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // 这个就是刚刚配置的xml文件名
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "QYS_Project.xml");
                // 默认的第二个参数是false，这个是controller的注释，记得修改
                c.IncludeXmlComments(xmlPath, true);

            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddHttpContextAccessor();
            services.AddScoped<CurUserHelp>();

            services.AddSingleton<QYSAuthorizeAttribute>();

            services.AddService();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, QYSDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QYS.Api v1"));

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate(); //执行迁移
            }

            //定时任务
            Machine.ServiceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            Machine.Start();
        }
    }
}
