using CoMoCo.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMoCo.Moves
{
    public abstract class BaseMove
    {
        public abstract string MovementName { get; }

        public abstract void ExecuteAction(hexapod hexy);
    }
}
