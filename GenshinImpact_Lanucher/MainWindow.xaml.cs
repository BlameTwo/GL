using GenshinImpact_Lanucher.Model;
using GenshinImpact_Lanucher.Utils;
using GenshinImpact_Lanucher.ViewModels;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFUI.Appearance;

namespace GenshinImpact_Lanucher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Window win { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            //过滤证书
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, error) =>
            {
                return true;
            };
            //ProxyController Proxy = new ProxyController("11451","127.0.0.1");
            //Proxy.Start();
            //Proxy.Stop();
            this.DataContext = new MainWinVM();
            Loaded += MainWindow_Loaded;
            win = this;

            myini = new Launcher_Ini($@"{Resource.docpath}/GSIConfig/Config/LauncherConfig.ini");
        }

        Launcher_Ini myini { get; set; }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Theme.Changed += Theme_Changed;    //Window11
            //设置上自动修改颜色，背景材质为默认
            switch (myini.IniReadValue("Style", "Theme"))
            {
                case "Auto":
                    WPFUI.Appearance.Watcher.Watch(this, BackgroundType.Mica, true, true);
                    break;
                case "Dark":
                    WPFUI.Appearance.Theme.Apply(
                        ThemeType.Dark,
                        backgroundEffect: WPFUI.Appearance.BackgroundType.Mica, true, true);
                    break;
                case "Light":
                    WPFUI.Appearance.Theme.Apply(
                        ThemeType.Light,
                        backgroundEffect: WPFUI.Appearance.BackgroundType.Mica, true, true);
                    break;
            }

            BackImage.Opacity = double.Parse(myini.IniReadValue("Style", "Tran"));
            WindowBlur.Radius = double.Parse(myini.IniReadValue("Style","Blur"));
            try
            {
                var bitmap = new BitmapImage(new Uri(myini.IniReadValue("Style", "BackImage")));
                bitmap.DecodePixelHeight = 1000;
                bitmap.DecodePixelWidth = 1000;
                BackImage.Source = bitmap;
            }
            catch (Exception)
            {
            }
            Console.WriteLine("完成启动器启动。");

            //调试过程中如果项目退出触发了无法联网的BUG，可以尝试取消注释下面的代码
            //ProxyController Proxy = new ProxyController("11451","127.0.0.1");
            //Proxy.Start();
            //Proxy.Stop();

        }

        private void Theme_Changed(ThemeType currentTheme, Color systemAccent)
        {
           
        }
    }
}
