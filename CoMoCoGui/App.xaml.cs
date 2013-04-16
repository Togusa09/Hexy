﻿using CoMoCoGui.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CoMoCoGui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            // Create the ViewModel and expose it using the View's DataContext
            //Views.MainView view = new Views.MainView();
            var viewModel = new MainWindowViewModel();
            var view = new MainWindow();
            view.DataContext = viewModel;
            view.Show();
        }
    }
}
