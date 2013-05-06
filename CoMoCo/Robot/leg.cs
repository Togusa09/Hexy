using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoMoCo.Robot
{
    public class leg
    {
        Controller _Controller;
        string _Name;
        int _HipServoNumber;
        int _KneeServoNumber;
        int _AnkleServoNumber;

        int _StepsPerSecond = 5;
        int _Floor = 60; //this is the minimum level the legs will reach

        public leg(Controller controller, string name, int hipServoNum, int kneeServoNum, int ankleServoNum)
        {
            _Controller = controller;
            _Name = name;
            _HipServoNumber = hipServoNum;
            _KneeServoNumber = kneeServoNum;
            _AnkleServoNumber = ankleServoNum;
        }

        public string GetStatus()
        {
            var hipAngle = _Controller.Servos[_HipServoNumber].PosDeg;
            var kneeAngle = _Controller.Servos[_KneeServoNumber].PosDeg;
            var ankleAngle = _Controller.Servos[_AnkleServoNumber].PosDeg;

            return string.Format("Name: {0} {1} {2} {3}", _Name, hipAngle, kneeAngle, ankleAngle);
        }

        public void hip(int? deg)
        {
            if (!deg.HasValue)
                _Controller.Servos[_HipServoNumber].kill();
            else
                _Controller.Servos[_HipServoNumber].setPos(deg);
        }

        public void knee(int? deg)
        {
            if (!deg.HasValue)
                _Controller.Servos[_KneeServoNumber].kill();
            else
                _Controller.Servos[_KneeServoNumber].setPos(deg);
        }

        public void ankle(int? deg)
        {
            if (!deg.HasValue)
                _Controller.Servos[_AnkleServoNumber].kill();
            else
                _Controller.Servos[_AnkleServoNumber].setPos(deg);
        }

        public void setHipDeg(int endHipAngle, float stepTime = 1)
        {
            // Runs a movement

            var thread = new Thread(new ThreadStart(() => setHipDeg_function(endHipAngle,stepTime)));
            thread.Start();
        }

        public void replantFoot(int endHipAngle, float stepTime = 1)
        {
            // Runs a movement
            var thread = new Thread(new ThreadStart(() => replantFoot_function(endHipAngle, stepTime)));
            thread.Start();
        }

        public void setFootY(int endHipAngle, float stepTime = 1)
        {
            // Runs a movement
            var thread = new Thread(new ThreadStart(() => setFootY_function(endHipAngle, stepTime)));
            thread.Start();
        }

        public void setHipDeg_function(int endHipAngle, float stepTime)
        {
            var currentHipAngle = _Controller.Servos[_HipServoNumber].PosDeg;
            var hipMaxDiff = endHipAngle - currentHipAngle;

            for (int i = 0; i < _StepsPerSecond; i++)
            {
                var hipAngle = (hipMaxDiff / _StepsPerSecond) * (i + 1);
                try
                {
                    var angleNorm = hipAngle * (180 / hipMaxDiff);
                }
                catch
                {
                    var angleNorm = hipAngle * (180 / 1);
                }
                hipAngle += currentHipAngle;
                _Controller.Servos[_HipServoNumber].setPos((int)hipAngle);

                // Wait for next cycle
                Thread.Sleep((int)(((float)stepTime / (float)_StepsPerSecond) * 1000));
            }
        }

        public void setFootY_function(int footY, float stepTime)
        {
            if (footY < 75 && footY > -75)
            {
                var kneeAngle = (Math.Asin((float)footY / 75) * (180.0 / Math.PI));
                var ankleAngle = 90 - kneeAngle;
                _Controller.Servos[_KneeServoNumber].setPos((int)kneeAngle);
                _Controller.Servos[_AnkleServoNumber].setPos(-(int)ankleAngle);
            }
        }

        public void replantFoot_function(int endHipAngle, float stepTime)
        {
            //Smoothly moves a foot from one position on the ground to another in time seconds
            var currentHipAngle = _Controller.Servos[_HipServoNumber].PosDeg;
            var hipMaxDiff = endHipAngle - currentHipAngle;

            // Caclulate the absolute distance between the foots highests and lowest point
            var footMax = 0;
            var footMin = _Floor;
            

            var footRange = Math.Abs(footMax - footMin);

            for (int i = 0; i < _StepsPerSecond; i++)
            {
                var hipAngle = (hipMaxDiff / _StepsPerSecond) * (i + 1);
                //Console.WriteLine("hip angle calc'd: " + hipAngle);    

                var angleNorm = 0.0f;

                // Normalize the range of the hip movement to 180 deg
                if (hipMaxDiff != 0)
                    angleNorm = hipAngle * (180 / hipMaxDiff);
                else
                    angleNorm = hipAngle * (180 / 1);
                
                //Console.WriteLine("Normalized angle: {0}", angleNorm);
                // Base footfall on a sin pattern from footfall to footfall with 0 as the midpoint
                var footY = footMin - Math.Sin((Math.PI * angleNorm) / 180.0) * footRange;
                //Console.WriteLine("Caclulated footY {0}", footY);

                // Set foot height
                setFootY((int)footY, stepTime = 0);
                hipAngle += currentHipAngle;

                _Controller.Servos[_HipServoNumber].setPos((int)hipAngle);
                Thread.Sleep((int)(((float)stepTime / (float)_StepsPerSecond) * 1000));
            }
        }
    }
}
