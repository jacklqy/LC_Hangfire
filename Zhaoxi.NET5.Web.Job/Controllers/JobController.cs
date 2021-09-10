using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhaoxi.NET5.Web.Job.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    { 
        [Route("Get01")]
        [HttpGet]
        public void Get01()
        {
            Console.WriteLine("***********************Get01**************************"); 
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")} 执行。。。");
        }

        [Route("Get02")]
        [HttpGet]
        public void Get02()
        {
            Console.WriteLine("***********************Get02**************************");
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")} 执行。。。");
        }

        [Route("Get03")]
        [HttpPost]
        public void Get03()
        {
            Console.WriteLine("***********************Get02**************************");
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")} 执行。。。");
        }
    }
}
