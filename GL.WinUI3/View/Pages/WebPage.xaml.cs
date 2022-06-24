using GL.WinUI3.GameNotifys;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyApp1.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WebPage : Page
    {
        public WebPage()
        {
            this.InitializeComponent();
        }

        Notice Data { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var data = e.Parameter as Notice;

            if (data != null)
            {
                this.Data = data;
                Load();
            }
            base.OnNavigatedTo(e);
        }

        private async void Load()
        {
            await MyWeb.EnsureCoreWebView2Async();
            MyWeb.NavigateToString(Data.Content);
        }
    }
}
