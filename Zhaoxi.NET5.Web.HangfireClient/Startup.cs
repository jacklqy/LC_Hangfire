using Hangfire;
using Hangfire.Console;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.HttpJob;
using Hangfire.SqlServer;
using Hangfire.Tags.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhaoxi.NET5.Web.HangfireClient.Utility;

namespace Zhaoxi.NET5.Web.HangfireClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            #region Hangfire配置
            //1.nuget引入:Hangfire.core
            //            Hangfire.SqlServer 
            #endregion
            services.AddHangfire(configura =>
            {
                //指定存储介质
                configura.UseSqlServerStorage("Data Source=.; Database=HangfireDB; User ID=sa; Password=123456; Integrated Security=True;", new SqlServerStorageOptions() //Nuget引入：
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }).UseTagsWithSql()//nuget引入Hangfire.Tags.SqlServer
            .UseConsole(new ConsoleOptions()
            {
                BackgroundColor = "#000079"
            }).UseHangfireHttpJob();

            });
            services.AddControllersWithViews();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            #region Hangfire
            app.UseHangfireServer();//启动HangfireServer
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                //这里是默认实现，如果使用这个，小心坑：nuget引入：Hangfire.Dashboard.BasicAuthorization
                Authorization = new BasicAuthAuthorizationFilter[] {
                      new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions(){
                           SslRedirect=false,
                            RequireSsl=false,
                             Users=new BasicAuthAuthorizationUser[]{
                                  new BasicAuthAuthorizationUser(){
                                      Login="admin",
                                      PasswordClear="test"
                                  }
                             }
                      })
                }


                /////关于Api授权部分，大家先研究一下；如果搞不定，再找我；
                //Authorization = new CustomDashboardAuthorizationFilter[] {
                //      new CustomDashboardAuthorizationFilter()
                //}

            });  //支持可视化界面 ---任何一个用户都能够来访问；所以需要加一道屏障

            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
