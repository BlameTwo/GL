using GenshinImpact_Lanucher.GameNotifys;
using GenshinImpact_Lanucher.Pages;
using GenshinImpact_Lanucher.UserControls;
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

namespace GenshinImpact_Lanucher.ItemsControlDT
{
    /// <summary>
    /// NitifyGameDT.xaml 的交互逻辑
    /// </summary>
    public partial class NitifyGameDT : UserControl
    {
        public NitifyGameDT()
        {
            InitializeComponent();
            
        }


        public Notice MyData
        {
            get { return (Notice)GetValue(MyDataProperty); }
            set { SetValue(MyDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyDataProperty =
            DependencyProperty.Register("MyData", typeof(Notice), typeof(NitifyGameDT), new PropertyMetadata(null));





        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NotifyMoreMessage messagebox = new NotifyMoreMessage();
            messagebox.MyNotice = MyData;
            messagebox.ShowDialog();
        }
    }
}
