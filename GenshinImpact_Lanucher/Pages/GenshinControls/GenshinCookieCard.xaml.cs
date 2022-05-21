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

namespace GenshinImpact_Lanucher.Pages.GenshinControls
{
    /// <summary>
    /// GenshinCookieCard.xaml 的交互逻辑
    /// </summary>
    public partial class GenshinCookieCard : Card
    {
        public GenshinCookieCard()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow)?.MyNavigation.Navigate("设置");
        }
    }
}
