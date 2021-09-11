# LC_Hangfire 官网：https://www.hangfire.io/
## Hangfire.HttpJob究竟是干嘛的
传统使用Hangfire都是把JOb的处理逻辑代码写在和Hangfire的同一个工程！

缺点： 这样就耦合在了一起，如果业务线增大，会导致每个业务线的Job处理逻辑都得和Hangfire耦合在一起！发布的时候所有业务线Job都得暂停调度

而使用了Hangfire.HttpJob的话 就是把Hangfire的服务拓展成可以把Job的处理逻辑代码写在别的工程里面(以webapi的形式暴露给Hangfire去调度)

优点：这样就解耦了Hangfire和业务处理逻辑，业务job开发者可以忽略Hangfire的存在！不同的业务线分开不同的JobAgent可以分别部署，发布互不影响

## Hangfire.HttpJob
是对Hangfire的一个扩展插件，利用Hangfire.HttpJob可以快速搭建分部署Job调度Server。

特点是：1业务与调度完全分离。2支持定点执行 延迟执行 周期性循环执行，支持秒级别。3配合JobAgent组件可以实现Job管理 监控 日志等

## Hangfire轻量级任务调度框架讲解 
![image](https://user-images.githubusercontent.com/26539681/132818091-06208174-120f-48b4-846e-e0cbdcb86e82.png)
![image](https://user-images.githubusercontent.com/26539681/132817978-51bb503a-41f2-43f6-973c-88d82d1986e4.png)
![image](https://user-images.githubusercontent.com/26539681/132818020-8c05033a-6e29-4f0e-a227-d24072acf04a.png)
