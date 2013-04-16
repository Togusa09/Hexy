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

        private int _ServoPosition = 1500;
        public int ServoPosition 
        {
            get {return _ServoPosition;}
            set 
            {
                if (value == _ServoPosition) return;
                _ServoPosition = value;
                OnPropertyChanged("ServoPosition");
            }
        }

        private bool _ServoActive = false;
        public bool ServoActive
        {
            get { return _ServoActive; }
            set
            {
                if (value == _ServoActive) return;
                _ServoActive = value;
                OnPropertyChanged("ServoActive");
            }
        }

        private ICommand _ResetServo;
        public ICommand ResetServo
        {
            get
            {
                if (_ResetServo == null)
                {
                    _ResetServo = new DelegateCommand(delegate()
                    {
                        ServoPosition = 5000;
                        ServoActive = true;
                    });
                }
                return _ResetServo;
            }
        }

        public ServoViewModel()
        {
            ServoMinPosition = 500;
            ServoMaxPosition = 2500;
            ServoPosition = 1500;
            ServoActive = false;
        }
    }
}
