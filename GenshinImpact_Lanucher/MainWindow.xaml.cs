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
            Closing += MainWindow_Closed; ;
            win = this;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var b = new ComputerInfo().OSFullName;
            if(b.IndexOf("11") != 0)
            {
                //Window11
                WPFUI.Appearance.Watcher.Watch(this, BackgroundType.Mica, true, true);
                
            }
            else if(b.IndexOf("7") != 0 ||b.IndexOf("8")!=0)
            {
                ChipBack.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                ChipBack.Background = new SolidColorBrush(Colors.Black);
            }

            Console.WriteLine("完成启动器启动。");
        }
    }
}
