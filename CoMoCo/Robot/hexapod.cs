using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoMoCo.Robot
{
    public class hexapod
    {
        private Controller _Controller;

        public leg RightFront;
        public leg RightMiddle;
        public leg RightBack;

        public leg LeftFront;
        public leg LeftMiddle;
        public leg LeftBack;

        public leg[] Legs;
        public leg[] Tripod1;
        public leg[] Tripod2;

        private neck _Neck;

        public hexapod(Controller controller)
        {
            _Controller = controller;

            RightFront = new leg(_Controller, "rightFront", 24, 25, 26);
            RightMiddle = new leg(_Controller, "rightMid", 20, 21, 22);
            RightBack = new leg(_Controller, "rightBack", 16, 17, 18);

            LeftFront = new leg(_Controller, "leftFront", 7, 6, 5);
            LeftMiddle = new leg(_Controller, "leftMid", 11, 10, 9);
            LeftBack = new leg(_Controller, "leftBack", 15, 14, 13);

            Legs = new leg[] 
            {
                RightFront, RightMiddle, RightBack,
                LeftFront, LeftMiddle, LeftBack
            };

            _Neck = new neck(_Controller, 31);
            Tripod1 = new leg[] { RightFront, RightBack, LeftMiddle };
            Tripod2 = new leg[] { LeftFront, LeftBack, RightMiddle };
        }

        public void GetUp()
        {
            var deg = -30;
            LeftFront.hip(-deg);
            RightMiddle.hip(1);
            LeftBack.hip(deg);

            RightFront.hip(deg);
            LeftMiddle.hip(1);
            RightBack.hip(-deg);

            Thread.Sleep(500);

            foreach (var leg in Legs)
            {
                leg.knee(-30);
            }

            Thread.Sleep(500);

            for (var angle = 0; angle < 45; angle += 3)
            {
                foreach (var leg in Legs)
                {
                    leg.knee(angle);
                    leg.ankle(-90 + angle);
                }
                Thread.Sleep(100);
            }
            Reset();
        }

        public void Reset()
        {
            var deg = -30;
            // pickup and put all the feet centered on the floor
            LeftFront.replantFoot(-deg, 0.3f);
            RightMiddle.replantFoot(1, 0.3f);
            LeftBack.replantFoot(deg, 0.3f);

            Thread.Sleep(500);

            RightFront.replantFoot(deg, 0.3f);
            LeftMiddle.replantFoot(1, 0.3f);
            RightBack.replantFoot(-deg, 0.3f);

            Thread.Sleep(500);

            // set all the hip angle to what they should be while standing
            LeftFront.hip(-deg);
            RightMiddle.hip(1);
            LeftBack.hip(deg);
            RightFront.hip(deg);
            LeftMiddle.hip(1);
            RightBack.hip(-deg);
        }

        public void SetZero()
        {
            foreach (var servo in _Controller.Servos)
            {
                servo.setPos(0);
            }
        }
    }
}
