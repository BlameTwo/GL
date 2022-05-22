using GenshinImpact_Lanucher.MiHaYouAPI;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GenshinImpact_Lanucher.Pages.GenshinControls
{
    /// <summary>
    /// MainGenshin.xaml 的交互逻辑
    /// </summary>
    public partial class MainGenshin : UserControl
    {
        public MainGenshin()
        {
            InitializeComponent();
            this.DataContext = new MainGenshinVM();
        }

        
    }
}
