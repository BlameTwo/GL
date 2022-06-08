using GenshinImpact_Lanuncher.ViewModels;
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
using WPFUI.Controls;

namespace GenshinImpact_Lanuncher.Pages
{
    /// <summary>
    /// NotifyGamePage.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyGamePage : Page
    {
        public NotifyGamePage()
        {
            InitializeComponent();
            this.DataContext = new NotifyGamePageVM();
            this.Unloaded += NotifyGamePage_Unloaded;
        }

        private void NotifyGamePage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.FindName("pages") != null)
                this.UnregisterName("pages");
        }

        private void NotifiMoreDialog_ButtonRightClick(object sender, RoutedEventArgs e)
        {
            var dialog = sender as Dialog;
            dialog.Hide();
        }
    }
}
