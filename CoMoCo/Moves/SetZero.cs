using CoMoCo.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMoCo.Moves
{
    class SetZero : BaseMove
    {
        public override string MovementName
        {
            get { return "SetZero"; }
        }

        public override void ExecuteAction(hexapod hexy)
        {
            foreach (var servo in hexy.Controller.Servos)
            {
                servo.setPos(0);
            }
        }
    }
}
