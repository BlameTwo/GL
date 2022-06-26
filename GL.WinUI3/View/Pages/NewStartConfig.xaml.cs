using GL.WinUI3;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyApp1.Dialog;
using MyApp1.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyApp1.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewStartConfig : Page
    {
        public NewStartConfig()
        {
            this.InitializeComponent();
            this.DataContext = vm;
        }
        NewStartConfigViewModel vm = new NewStartConfigViewModel();
        private async  void Button_Click(object sender, RoutedEventArgs e)
        {
            StartGameConfig dialog = new StartGameConfig();
            dialog.XamlRoot = (App.MainWindow.Content as MainPage).XamlRoot;
            await dialog.ShowAsync();
        }
    }
}
