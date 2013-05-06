using CoMoCo;
using CoMoCo.Robot;
using CoMoCoGui.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoMoCoGui.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ServoViewModel> Servos { get; set; }
        public ObservableCollection<ServoViewModel> Servos2 { get; set; }

        public MainWindowViewModel()
        {
            Servos = new ObservableCollection<ServoViewModel>();
            Servos2 = new ObservableCollection<ServoViewModel>();

            var controller = new Controller(32);
            var hexy = new hexapod(controller);

            for (int i = 0; i < 16; i++)
            {
                Servos.Add(new ServoViewModel(i, controller.Servos[i]));
                Servos2.Add(new ServoViewModel(i + 16, controller.Servos[i + 16]));
            }
        }

        private ICommand _StopAllServos;
        public ICommand StopAllServos
        {
            get
            {
                if (_StopAllServos == null)
                {
                    _StopAllServos = new DelegateCommand(delegate()
                    {
                        foreach (var servo in Servos2)
                        {
                            servo.ResetServo();
                        }
                        foreach (var servo in Servos2)
                        {
                            servo.ResetServo();
                        }
                    });
                }
                return _StopAllServos;
            }
        }

        public void Window_Closing(object sender, CancelEventArgs e)
        {
            //e.Cancel = true;
            foreach (var servo in Servos2)
            {
                servo.ServoActive = false;
            }
            foreach (var servo in Servos2)
            {
                servo.ServoActive = false;
            }
        }
    }
}
