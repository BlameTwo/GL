using GenshinImpact_Lanucher.Model;
using GenshinImpact_Lanucher.ViewModels;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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
            this.DataContext = new MainWinVM();
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
            win = this;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            if (StartGame.Controller != null) { }
                StartGame.Controller.Stop();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Theme.Changed += Theme_Changed;    //Window11
            //设置上自动修改颜色，背景材质为默认
            WPFUI.Appearance.Watcher.Watch(this, BackgroundType.Auto, true, true);
            Console.WriteLine("完成启动器启动。");
        }

        private void Theme_Changed(ThemeType currentTheme, Color systemAccent)
        {
            if(currentTheme == ThemeType.Light)
            {
                ChipBack.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                ChipBack.Background = new SolidColorBrush(Colors.Black);
            }
        }
    }
}
