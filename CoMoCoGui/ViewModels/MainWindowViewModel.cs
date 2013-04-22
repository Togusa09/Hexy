using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMoCoGui.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ServoViewModel> Servos { get; set; }

        public MainWindowViewModel()
        {
            Servos = new ObservableCollection<ServoViewModel>();
            for (int i = 0; i < 32; i++)
            {
                Servos.Add(new ServoViewModel(i));
            }
        }
    }
}
