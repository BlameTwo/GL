using GenshinImpact_Lanuncher.Model;
using GenshinImpact_Lanuncher.Utils;
using GenshinImpact_Lanuncher.ViewModels;
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

namespace GenshinImpact_Lanuncher
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


            this.DataContext = new MainWinVM();
            Loaded += MainWindow_Loaded;
            win = this;

            myini = new Launcher_Ini($@"{Resource.docpath}/GSIConfig/Config/LauncherConfig.ini");
        }

        Launcher_Ini myini { get; set; }
        string Themes = "";
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Theme.Changed += Theme_Changed;    //Window11
            string IsMica = "";
            var ismaica = System.Convert.ToBoolean(myini.IniReadValue("Style", "IsMica"));

            switch (myini.IniReadValue("Style", "Theme"))
            {
                case "Auto":
                    Themes = "Auto";
                    break;
                case "Dark":
                    Themes = "Dark";
                    break;
                case "Light":
                    Themes = "Light";
                    break;
            }
            if (ismaica == true)
            {
                //这一段是专门对于Win11，且开启Mica特效
                Theme.Apply(
                        Themes == "Dark"? ThemeType.Dark: ThemeType.Light,
                        BackgroundType.Mica, true, true);
                return;
            }
            else
            {
                if(Themes =="Auto")
                    WPFUI.Appearance.Watcher.Watch(this, BackgroundType.Auto, true, true);
                else
                {
                    Theme.Apply(
                        Themes == "Dark" ? ThemeType.Dark : ThemeType.Light,
                        BackgroundType.Auto, true, true);
                }
                
                
                BackImage.Opacity = double.Parse(myini.IniReadValue("Style", "Tran"));
                WindowBlur.Radius = double.Parse(myini.IniReadValue("Style", "Blur"));
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
            }
        }

        private void Theme_Changed(ThemeType currentTheme, Color systemAccent)
        {
           
        }
    }
}
