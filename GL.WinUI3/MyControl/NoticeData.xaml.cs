using GL.WinUI3;
using GL.WinUI3.GameNotifys;
using GL.WinUI3.WindowHelper;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Web.WebView2.Core;
using MyApp1.View;
using MyApp1.View.Pages;
using MyApp1.WindowHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyApp1.MyControl
{
    public sealed partial class NoticeData : UserControl
    {
        public NoticeData()
        {
            this.InitializeComponent();
        }





        public Notice MyData
        {
            get 
            {
                object obj = GetValue(MyDataProperty);
                if((Notice)obj != null)
                {
                    if (!string.IsNullOrWhiteSpace(((Notice)obj).banner))
                    {
                        image.Source = new BitmapImage(new Uri(MyData.banner));
                        return (Notice)obj;
                    }
                }
                return (Notice)obj;
            }
            set 
            { 
                SetValue(MyDataProperty, value); 
            }
        }

        // Using a DependencyProperty as the backing store for MyData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyDataProperty =
            DependencyProperty.Register("MyData", typeof(Notice), typeof(NoticeData),new PropertyMetadata(default(Notice)));

        private void UserControl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            MoveTrue.Begin();
        }
        

        private void UserControl_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            MoveFalse.Begin();
        }

        private void UserControl_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            NavigationHelper helper = new NavigationHelper();
            INavigations navigations = new INavigations();
            navigations.MyAction = () =>
            {
                (App.MainWindow.Content as MainPage).MyFrame.Navigate(typeof(WebPage), MyData, new DrillInNavigationTransitionInfo());

            };
            navigations.Message = "跳转到公告详情页面";
            helper.GO(navigations);
        }
    }
}
