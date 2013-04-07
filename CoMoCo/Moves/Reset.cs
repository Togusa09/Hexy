using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoMoCo.Moves
{
    class Reset : BaseMove
    {
        public override string MovementName
        {
            get { return "Reset"; }
        }

        public override void ExecuteAction(Robot.hexapod hexy)
        {
            var deg = -30;
            // pickup and put all the feet centered on the floor
            hexy.LeftFront.replantFoot(-deg, 0.3f);
            hexy.RightMiddle.replantFoot(1, 0.3f);
            hexy.LeftBack.replantFoot(deg, 0.3f);

            Thread.Sleep(500);

            hexy.RightFront.replantFoot(deg, 0.3f);
            hexy.LeftMiddle.replantFoot(1, 0.3f);
            hexy.RightBack.replantFoot(-deg, 0.3f);

            Thread.Sleep(500);

            // set all the hip angle to what they should be while standing
            hexy.LeftFront.hip(-deg);
            hexy.RightMiddle.hip(1);
            hexy.LeftBack.hip(deg);
            hexy.RightFront.hip(deg);
            hexy.LeftMiddle.hip(1);
            hexy.RightBack.hip(-deg);
        }
    }
}
