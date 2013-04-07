using CoMoCo;
using CoMoCo.Robot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HexyDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            //var serHandler = new serHandler();
            var controller = new Controller(32);
            var hexy = new hexapod(controller);

            Thread.Sleep(3000);

            //hexy.GetUp();
            hexy.SetZero();
            
            
            controller.killAll();
            
        }
    }

}
