using CoMoCo;
using CoMoCoGui.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoMoCoGui.ViewModels
{
    class ServoViewModel : ViewModelBase
    {

        public int ServoMinPosition { get; set; }
        public int ServoMaxPosition { get; set; }
        public int ServoNumber { get; set; }
        private Servo _Servo;

        //private int _ServoPosition = 1500;
        public int ServoPosition 
        {
            get {return _Servo.PosuS;}
            set 
            {
                if (value == _Servo.PosuS) return;
                _Servo.setPos(null, value, ServoActive);
                OnPropertyChanged("ServoPosition");
            }
        }

        public bool ServoActive
        {
            get { return _Servo.Active; }
            set
            {
                if (value == _Servo.Active) return;

                if (value == false)
                    _Servo.kill();
                else
                    _Servo.setPos(null, null, true);

                OnPropertyChanged("ServoActive");
            }
        }

        private ICommand _ResetServoCommand;
        public ICommand ResetServoCommand
        {
            get
            {
                if (_ResetServoCommand == null)
                {
                    _ResetServoCommand = new DelegateCommand(delegate()
                    {
                        ResetServo();
                    });
                }
                return _ResetServoCommand;
            }
        }

        public void ResetServo()
        {
            ServoPosition = 1500;
            ServoActive = true;
        }

        public ServoViewModel(int number, Servo servo)
        {
            _Servo = servo;
            ServoNumber = number;
            ServoActive = false;
            ServoMinPosition = 500;
            ServoMaxPosition = 2500;
            ServoPosition = 1500;
            
            
        }
    }
}
