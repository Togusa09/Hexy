using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoMoCo.Moves
{
    class MoveForward : BaseMove
    {
        public override string MovementName
        {
            get { return "MoveForward"; }
        }

        public override void ExecuteAction(Robot.hexapod hexy)
        {
            var deg = 25;
            var midFloor = 30;
            var hipSwing = 25;
            var pause = 0.5;

            for (int timeStop = 0; timeStop < 2; timeStop++)
            {
                //Thread.Sleep(100);
                // replant tripod2 forward while tripod1 move behind
                //   relpant tripod 2 forward

                hexy.LeftFront.replantFoot(deg - hipSwing, 0.5f);
                hexy.RightMiddle.replantFoot(hipSwing, 0.5f);
                hexy.LeftBack.replantFoot(-deg - hipSwing, 0.5f);

                // Tripod 1 moves behind
                hexy.RightFront.setHipDeg(-deg - hipSwing, 0.5f);
                hexy.LeftMiddle.setHipDeg(hipSwing, 0.5f);
                hexy.RightBack.setHipDeg(deg - hipSwing, 0.5f);

                // Replant tripod1 forward while tripod 2 move behind
                // replacnt tripod 1 forward

                hexy.RightFront.replantFoot(-deg + hipSwing, 0.5f);
                hexy.LeftMiddle.replantFoot(-hipSwing, 0.5f);
                hexy.RightBack.replantFoot(deg - hipSwing, 0.5f);

                // Tripod2 moves behind
                hexy.LeftFront.setHipDeg(deg + hipSwing, 0.5f);
                hexy.RightMiddle.setHipDeg(-hipSwing, 0.5f);
                hexy.LeftBack.setHipDeg(-deg + hipSwing, 0.5f);
                Thread.Sleep(600);
            }
        }
    }
}
