using CoMoCo.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoMoCo.Robot
{
    public class hexapod
    {
        public Controller Controller;

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

        List<BaseMove> _Movements;

        public hexapod(Controller controller)
        {
            Controller = controller;

            RightFront = new leg(Controller, "rightFront", 24, 25, 26);
            RightMiddle = new leg(Controller, "rightMid", 20, 21, 22);
            RightBack = new leg(Controller, "rightBack", 16, 17, 18);

            LeftFront = new leg(Controller, "leftFront", 7, 6, 5);
            LeftMiddle = new leg(Controller, "leftMid", 11, 10, 9);
            LeftBack = new leg(Controller, "leftBack", 15, 14, 13);

            Legs = new leg[] 
            {
                RightFront, RightMiddle, RightBack,
                LeftFront, LeftMiddle, LeftBack
            };

            _Neck = new neck(Controller, 31);
            Tripod1 = new leg[] { RightFront, RightBack, LeftMiddle };
            Tripod2 = new leg[] { LeftFront, LeftBack, RightMiddle };

            var baseType = typeof(BaseMove);
            _Movements = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => baseType.IsAssignableFrom(t)
                    && t != baseType)
                .Select(t => (BaseMove)Activator.CreateInstance(t))
                .ToList();
        }

        public void Move(string moveName)
        {
            var movement = _Movements.Where(m => m.MovementName == moveName)
                .FirstOrDefault();
            if (movement != null)
            {
                movement.ExecuteAction(this);
                Console.WriteLine("Executing action {0}", movement.MovementName);
            }
        }
    }
}
