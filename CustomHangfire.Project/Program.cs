using Hangfire;
using Hangfire.SqlServer;
using System;

namespace CustomHangfire.Project
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //1.nuget引入:Hangfire.core
                //            Hangfire.SqlServer 
                GlobalConfiguration.Configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_110) //设置兼容性---兼容的版本
                    .UseColouredConsoleLogProvider()
                    .UseSimpleAssemblyNameTypeSerializer()////使用简单程序集名称类型序列化程序
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage("Data Source=DESKTOP-63QE7M1; Database=Hangfire.Sample; User ID=sa; Password=sa123; Integrated Security=True;", new SqlServerStorageOptions()
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5), //超时时间
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),//滑动超时
                        QueuePollInterval = TimeSpan.Zero,  //队列轮训间隔
                        UseRecommendedIsolationLevel = true, //
                        UsePageLocksOnDequeue = true, //出列时是需要页锁
                        DisableGlobalLocks = true // 是否禁用全局锁
                    });

                ///添加任务----把认为序列化；保存到数据库中去；
                ///因为表达式目录树是一个数据结构：要做的事情可以以一个数据结构的方式来存储；表达式目录树方便序列化；
                BackgroundJob.Enqueue(() => Console.WriteLine("你好"));

                ///最小的时间间隔为一分钟；
                ///如果：处理业务的代码较多；一行代码肯定不行啊
                ///
                RecurringJob.AddOrUpdate(() => Console.WriteLine($"开始{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}"), Cron.MinuteInterval(1));

                RecurringJob.AddOrUpdate<IEmailHelper>(email=> email.Send("186727132698@163.com"), Cron.MinuteInterval(1));
                 

                //当前启动以后，定点执行；确定多长时间以后执行；
                var jobId1 = BackgroundJob.Schedule(() => Console.WriteLine("Delayed!"), TimeSpan.FromMinutes(1));

                ///在jobId1执行完毕以后，马上执行；
                BackgroundJob.ContinueJobWith(jobId1, () => Console.WriteLine("Continuation!"));


                using (var server = new BackgroundJobServer())
                {
                    server.Start();//
                    Console.Read();
                }


               

                Console.Read();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
