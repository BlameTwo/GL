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
                //Windows7、Window8/Window8.1，根据WPF-UI官方文档修改
                this.Background = new SolidColorBrush(Color.FromArgb(255, 30, 30, 30));
                var newTheme = WPFUI.Appearance.Theme.GetAppTheme() == WPFUI.Appearance.ThemeType.Dark
                        ? WPFUI.Appearance.ThemeType.Light
            :               WPFUI.Appearance.ThemeType.Dark;
                WPFUI.Appearance.Theme.Apply(
                            themeType: ThemeType.Dark,
                            backgroundEffect: WPFUI.Appearance.BackgroundType.Unknown,
                            updateAccent: true,
                            forceBackground: false);
            }
            else
            {
                //Window10
                WPFUI.Appearance.Watcher.Watch(this, BackgroundType.Acrylic, true, true);
                //微透明白色
                this.Background = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));
            }
        }
    }
}
