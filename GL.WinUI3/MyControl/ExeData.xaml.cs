using GL.WinUI3.EventArgs;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyApp1.Converter;
using MyApp1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyApp1.MyControl
{
    public sealed partial class ExeData : UserControl
    {
        public ExeData()
        {
            this.InitializeComponent();
        }






        public ExeConfig MyData
        {
            get { return (ExeConfig)GetValue(MyDataProperty); }
            set { SetValue(MyDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyDataProperty =
            DependencyProperty.Register("MyData", typeof(ExeConfig), typeof(ExeData), new PropertyMetadata(null,new PropertyChangedCallback(async (s, e) =>
            {
                var Myo = s as ExeData;
                var value = e.NewValue as ExeConfig;
                Myo.MyIcon.Source = await IconConvert.Convert(value.Path);
            })));

        private void UserControl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            MySelect.Begin();
        }

        private void UserControl_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            MySelect2.Begin();
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            ExeArgs args = new ExeArgs()
            {
                Config = MyData,
                ExeEnum = ExeEnum.Remove
            };
            WeakReferenceMessenger.Default.Send(args);
        }
    }
}
