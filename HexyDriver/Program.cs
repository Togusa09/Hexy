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
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //var serHandler = new serHandler();
            var controller = new Controller(32);
            var hexy = new hexapod(controller);

            Thread.Sleep(2000);

            //hexy.GetUp();
            //hexy.SetZero();

            hexy.Move("GetUp");
            Thread.Sleep(2000);
            hexy.Move("MoveForward");
            Thread.Sleep(2000);
            hexy.Move("MoveForward");

            //hexy.Move("SetZero");
            //Thread.Sleep(500);

            //hexy.Move("GetUp");
            //Thread.Sleep(500);
            //hexy.Move("MoveForward");

            //while (true)
            //{
            //    hexy.Move("MoveForward");
            //    Thread.Sleep(3000);
            //}
            
            
            controller.killAll();
            
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            
        }
    }

}
