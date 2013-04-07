using CoMoCo.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoMoCo.Moves
{
    class BellyFlop : BaseMove
    {
        public override string MovementName
        {
            get { return "BellyFlop"; }
        }

        public override void ExecuteAction(hexapod hexy)
        {
            hexy.Move("SetZero");
            Thread.Sleep(2000);

            hexy.Move("GetUp");
            Thread.Sleep(500);
        }
    }
}
