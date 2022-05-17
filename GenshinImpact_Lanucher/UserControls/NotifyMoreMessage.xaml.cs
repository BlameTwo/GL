using GenshinImpact_Lanucher.GameNotifys;
using GenshinImpact_Lanucher.Models;
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
namespace GenshinImpact_Lanucher.UserControls
{
    /// <summary>
    /// NotifyMoreMessage.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyMoreMessage : WPFUI.Controls.MessageBox
    {
        public NotifyMoreMessage()
        {
            InitializeComponent();
        }



        public Notice MyNotice
        {
            get { return (Notice)GetValue(MyNoticeProperty); }
            set { SetValue(MyNoticeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyNotice.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyNoticeProperty =
            DependencyProperty.Register("MyNotice", typeof(Notice), typeof(NotifyMoreMessage), new PropertyMetadata(null,new PropertyChangedCallback((s, e) => {
                var value = s as NotifyMoreMessage;
                value.Flow2.Blocks.Add(HtmlFormat.FrrmatToFloutView(HtmlFormat.Format((e.NewValue as Notice).Content)));
                value.NotifyText.Text = (e.NewValue as Notice).title;
            })));

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MessageBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
