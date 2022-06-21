using GL.WinUI3.EventArgs;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyApp1.MyControl;
using MyApp1.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyApp1.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ServerGame : Page
    {
        public ServerGame()
        {
            this.InitializeComponent();
            
        }

        ServerGameViewModel vm = new ServerGameViewModel();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if ( this.MyGridView.Items.Count == 0)
            {
                vm.Page_Loaded(null, null);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(App.helper!= null)
                App.helper.RunCMD("stop");
        }
    }
}
