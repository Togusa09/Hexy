using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMoCo
{
    public class Servo
    {
        private serHandler _ServoHandler;
        private bool _Active;
        private int _ServoNumber;
        private int _ServoPosition;
        private int _Offset;

        public float PosDeg { get { return (float)(_ServoPosition - 1500) / 11.1111111f; } }
        public int PosuS { get { return _ServoPosition; } }
        public bool Active { get { return _Active; } }
        public float OffsetDeg 
        { 
            get { return (float)(_Offset - 1500) / 11.1111111f; }
            set { _Offset = (int)((float)value * 11.1111111f); }
        }
        public int OffsetuS 
        { 
            get { return _Offset; }
            set { _Offset = value; }
        }

        public Servo(int servoNum, serHandler serHandler, int servoPos = 1500, int offset = 0, bool active = false)
        {
            _ServoHandler = serHandler;
            _Active = active;
            _ServoNumber = servoNum;

            // Servo position and offset is stored in microseconds (uS)
            _ServoPosition = servoPos;
            _Offset = offset;
        }

        public void reset()
        {
            setPos(1500);
            move();
        }

        public void kill()
        {
            _Active = false;
            var toSend = "#" + _ServoNumber + "L\r";

            _ServoHandler.AddToSendQueue(toSend);
#if DEBUG
            Console.WriteLine("Sending command #" + _ServoNumber + "L to queue");
#endif
        }

        public void setPos(int? deg = null, int? timing = null, bool move = true) 
       {
           //int? timing = null;
            if (timing != null)
                _ServoPosition = timing.Value;
            if (deg != null)
                _ServoPosition = (int)(1500.0f + (float)(deg) * 11.1111111f);
            if (move)
            {
                _Active = true;
                this.move();
#if DEBUG
                Console.WriteLine("moved " + _ServoNumber);
#endif
            }
#if DEBUG
            Console.WriteLine("Servo " + _ServoNumber + " set to " + _ServoPosition);
#endif
        }

        public void move()
        {
            if (_Active)
            {
                var servoPosition = _ServoPosition + _Offset;
                //auto-correct the output to bound within 500uS to 2500uS signals, the limits of the servos
                if (servoPosition < 500)
                    servoPosition = 500;
                if (servoPosition > 2500)
                    servoPosition = 2500;

                // debug message if needed
#if DEBUG
                Console.WriteLine(string.Format("sending command #{0}P{1}T to queue",
                    _ServoNumber, servoPosition));
#endif
                // send the message to the serial handler in a thread safe manner
                var toSend = string.Format("#{0}P{1:D4}T0\r", _ServoNumber, servoPosition);
                _ServoHandler.AddToSendQueue(toSend);                
            }
            else
            {
                try
                {
                var toSend = string.Format("#{0}L\r", _ServoNumber);
                    //Send to the serial handler
#if DEBUG
                Console.WriteLine("Sending command #" + _ServoNumber + "L to queue");
#endif
                _ServoHandler.AddToSendQueue(toSend);
                }
                catch {}
            }
        }
    }
}
