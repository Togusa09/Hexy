using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoMoCo
{
    public class Controller
    {
        private serHandler _SerialHandler;
        public Servo[] Servos;

        public Controller(int servos = 32)
        {
            _SerialHandler = new serHandler();
            var timeout = DateTime.Now;
            while( !(_SerialHandler.serOpen || DateTime.Now - timeout > new TimeSpan(0, 0, 10)))
            {
                Thread.Sleep(10);
            }
            if (!_SerialHandler.serOpen)
            {
                Console.WriteLine("Connection to Servotor failed. No robot movement will occur.");
            }
            Console.WriteLine("initializing servos");
            Servos = new Servo[servos];
            for (int i = 0; i < servos; i++)
            {
                Servos[i] = new Servo(i, _SerialHandler);
                Servos[i].kill();
            }
        }

        public void Dispose()
        {
            _SerialHandler.Dispose();
        }

        public void killAll()
        {
            if (_SerialHandler.serOpen)
            {
                foreach (var servo in Servos)
                {
                    servo.kill();
                }
                Console.WriteLine("Killing all servos");
            }
        }
    }
}
