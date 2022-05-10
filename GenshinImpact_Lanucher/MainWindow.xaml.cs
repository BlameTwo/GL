using GenshinImpact_Lanucher.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
             WPFUI.Appearance.Watcher.Watch(this, BackgroundType.Mica, true, true);
        }
    }
}
