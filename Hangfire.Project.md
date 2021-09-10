# Hangfire

1.开始实操：nuget引入

```
dotnet add package Hangfire.Core
dotnet add package Hangfire.SqlServer
```

2.手动创建数据库，注意：数据库名称必须和下面  的配置文件中配置的一致方可

3.配置生成数据库表结构：

~~~
  GlobalConfiguration.Configuration
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
               .UseColouredConsoleLogProvider() //使用彩色控制台日志提供程序
               .UseSimpleAssemblyNameTypeSerializer()  //使用简单程序集名称类型序列化程序
               .UseRecommendedSerializerSettings() //使用推荐的序列化程序设置

               //使用SQLServerStorage
               .UseSqlServerStorage("Data Source=DESKTOP-63QE7M1; Database=Hangfire.Sample; User ID=sa; Password=sa123; Integrated Security=True;", new SqlServerStorageOptions
               {
                   CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                   SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                   QueuePollInterval = TimeSpan.Zero,
                   UseRecommendedIsolationLevel = true,
                   UsePageLocksOnDequeue = true,
                   DisableGlobalLocks = true
               });
~~~

4.