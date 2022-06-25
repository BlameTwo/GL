using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MyApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.WinUI3.ViewModel
{
    public class MainPageViewModel:ObservableRecipient
    {
        public MainPageViewModel()
        {
            IsActive = true;
        }


        public RelayCommand ShowApp { get; set; } = new RelayCommand(() =>
        {
            App.MainWindow.Activate();
        });

        public RelayCommand CloseApp { get; set; } = new RelayCommand(() =>
        {
            System.Environment.Exit(0);
        });
    }
}
