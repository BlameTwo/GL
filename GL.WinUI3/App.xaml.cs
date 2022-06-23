using GL.WinUI3;
using GL.WinUI3.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using MyApp1.Models;
using MyApp1.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyApp1
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.UnhandledException += App_UnhandledException;
        }

        private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            
        }

        void StopServer()
        {
            if (App.helper != null)
            {
                App.helper.RunCMD("stop");
                App.helper.Stop();
            }
        }

        public static Window MainWindow { get; set; }
        public static CMD_Helper helper { get; set; }
        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWin();
            MainWindow = m_window;

            MainWindow.Content = new MainPage();
            Launcher_Ini ini = new Launcher_Ini(Resource.docpath + @"\GSIConfig\Config\LauncherConfig.ini");
            MainWindow.Activate();
            Directory.CreateDirectory(Resource.docpath+@"\GSIConfig");
            Directory.CreateDirectory(Resource.docpath + @"\GSIConfig\Config");
            App.MainWindow.Closed += MainWindow_Closed;
            if (!File.Exists(Resource.docpath + @"\GSIConfig\Config\LauncherConfig.ini"))
            {
                ini.SaveDefaultSettingArgs();
            }
        }

        private void MainWindow_Closed(object sender, WindowEventArgs args)
        {
            StopServer();
        }

        private Window m_window;
    }
}
