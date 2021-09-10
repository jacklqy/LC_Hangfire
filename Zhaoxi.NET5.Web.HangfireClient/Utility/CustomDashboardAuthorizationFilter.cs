using Hangfire.Annotations;
using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhaoxi.NET5.Web.HangfireClient.Utility
{
    /// <summary>
    /// 也可以自这里做鉴权授权
    /// </summary>
    public class CustomDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            //在这里就可以去数据库中去验证
            return true;
        }
    }
}
