using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoMoCo.Moves
{
    class GetUp : BaseMove
    {
        public override string MovementName
        {
            get { return "GetUp"; }
        }

        public override void ExecuteAction(Robot.hexapod hexy)
        {
            var deg = -30;
            hexy.LeftFront.hip(-deg);
            hexy.RightMiddle.hip(1);
            hexy.LeftBack.hip(deg);

            hexy.RightFront.hip(deg);
            hexy.LeftMiddle.hip(1);
            hexy.RightBack.hip(-deg);

            Thread.Sleep(500);

            foreach (var leg in hexy.Legs)
            {
                leg.knee(-30);
            }

            Thread.Sleep(500);

            for (var angle = 0; angle <= 45; angle += 3)
            {
                foreach (var leg in hexy.Legs)
                {
                    leg.knee(angle);
                    leg.ankle(-90 + angle);
                }
                Thread.Sleep(100);
            }

            hexy.Move("Reset");
        }
    }
}
