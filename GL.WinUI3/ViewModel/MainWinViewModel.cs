using GL.WinUI3;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using MyApp1.EventArgs;
using MyApp1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp1.ViewModel
{
    public class MainWinViewModel:ObservableRecipient
    {
        public MainWinViewModel()
        {
            IsActive = true;
        }

        public void Receive(NavigationEvent message)
        {
            switch (message.NavigationPage)
            {
                case Navigations.Play:
                    (App.MainWindow as MainWin).MyFrame.Navigate(typeof(DefaultGame));
                    break;
                case Navigations.Server:
                    (App.MainWindow as MainWin).MyFrame.Navigate(typeof(ServerGame));
                    break;
                case Navigations.Notify:

                    break;
                case Navigations.MHY:
                    break;
                case Navigations.Setting:

                    (App.MainWindow as MainWin).MyFrame.Navigate(typeof(SettingPage));
                    break;
            }
        }
    }
}
