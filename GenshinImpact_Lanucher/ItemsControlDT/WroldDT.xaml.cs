using GenshinImpact_Lanucher.MiHaYouAPI;
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

namespace GenshinImpact_Lanucher.ItemsControlDT
{
    /// <summary>
    /// WroldDT.xaml 的交互逻辑
    /// </summary>
    public partial class WroldDT : UserControl
    {
        public WroldDT()
        {
            InitializeComponent();
        }




        public GenshinWorld MyData
        {
            get { return (GenshinWorld)GetValue(MyDataProperty); }
            set { SetValue(MyDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyDataProperty =
            DependencyProperty.Register("MyData", typeof(GenshinWorld), typeof(WroldDT), new PropertyMetadata(default(GenshinWorld),new PropertyChangedCallback((s, e) =>
            {
                var sender = s as WroldDT;
                var value = (e.NewValue as GenshinWorld);
                DoubleAnimation da = new DoubleAnimation() { From = 0, To = value.Progess };
                da.EasingFunction = new CubicEase()
                {
                    EasingMode = EasingMode.EaseOut
                };
                da.Duration = new Duration(new TimeSpan(0, 0, 0, 1, 500));
                sender.Pro1.BeginAnimation(ProgressBar.ValueProperty, da);
            })));

    }
}
