using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomHangfire.Project
{
    interface IEmailHelper
    {
        public void Send(string emailAdress)
        {
            Console.WriteLine("在这里可以写要处理的业务逻辑爱，爱写多少写多少；");
        }
    }
}
