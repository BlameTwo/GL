using GL.WinUI3;
using GL.WinUI3.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyApp1.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += (s, e) =>
            {
                App.MainWindow.SetTitleBar(AppTitleBar);
            };
            this.DataContext = VM;
        }
        MainPageViewModel VM = new MainPageViewModel();
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                MyFrame.Navigate(typeof(SettingPage), new DrillInNavigationTransitionInfo());
                return;
            }
            Type type = null;
            if (args.SelectedItem as NavigationViewItem == StartGame!)
            {
                type = typeof(DefaultGame);
            }
            if (args.SelectedItem as NavigationViewItem == Server!)
            {
                type = typeof(ServerGame);
            }
            if(args.SelectedItem as NavigationViewItem == Notify!)
            {
                type = typeof(NotifyPage);
            }
            Navigation.AlwaysShowHeader = false;
            Navigation.IsTitleBarAutoPaddingEnabled = false;
            if (type != null)
                MyFrame.Navigate(type, new DrillInNavigationTransitionInfo());

        }

        private void Navigation_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (MyFrame.CanGoBack)
                MyFrame.GoBack();
        }

        private void MyFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (MyFrame.CanGoBack)this.Navigation.IsBackEnabled = true;
        }
    }
}
