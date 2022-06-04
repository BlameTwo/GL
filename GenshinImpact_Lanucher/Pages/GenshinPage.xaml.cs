using GenshinImpact_Lanucher.Pages.GenshinControls;
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

namespace GenshinImpact_Lanucher.Pages
{
    /// <summary>
    /// GenshinPage.xaml 的交互逻辑
    /// </summary>
    public partial class GenshinPage : Page
    {
        public GenshinPage()
        {
            InitializeComponent();
            this.DataContext = new GenshinVM();
            Loaded += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace((DataContext as GenshinVM).myini.IniReadValue("MyLanucherConfig", "Cookie")))
                {
                    this.Content = new GenshinCookieCard();

                }
                else
                {
                    this.Content = new MainGenshin();
                    string cookie = (DataContext as GenshinVM).myini.IniReadValue("MyLanucherConfig", "Cookie");
                    MiHaYouAPI.MiHaYouArgs.Cookie = cookie;
                }
            };
        }
    }
}
