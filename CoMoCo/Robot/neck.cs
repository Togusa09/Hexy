using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMoCo.Robot
{
    class neck
    {
        Controller _Controller;
        int _servoNum;

        public neck(Controller controller, int servoNum)
        {
            _Controller = controller;
            _servoNum = servoNum;
        }

        public void set(int deg)
        {
            _Controller.Servos[_servoNum].setPos(deg);
        }
    }
}
