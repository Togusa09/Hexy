using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoMoCo
{
    public delegate void MoveFunction(int  endAngle, float stepTime);

    class runMovement
    {
        Thread _Thread;
        MoveFunction _Function;

        int _EndAngle;
        float _StepTime;

        public runMovement(MoveFunction function, int endAngle, float stepTime)
        {
            _Thread = new Thread(new ThreadStart(_ThreadAction));
            _Function = function;
            _EndAngle = endAngle;
            _StepTime = stepTime;
            _Thread.Start();
        }

        public void _ThreadAction()
        {
            _Function(_EndAngle, _StepTime);
        }
    }
}
