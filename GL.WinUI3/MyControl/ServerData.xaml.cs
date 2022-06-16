using GenshinImpact_Lanuncher.Utils;
using GL.WinUI3.EventArgs;
using GL.WinUI3.Model;
using Microsoft.Toolkit.Mvvm.Messaging;
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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyApp1.MyControl
{
    public sealed partial class ServerData : UserControl
    {
        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Launcher_Ini myini { get; set; }
        public ServerData()
        {
            this.InitializeComponent();
            this.myini = new Launcher_Ini($@"{Resource.docpath}/GSIConfig/Config/LauncherConfig.ini");
        }



        public ProxyArgs MyData
        {
            get { return (ProxyArgs)GetValue(MyDataProperty); }
            set { SetValue(MyDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyDataProperty =
            DependencyProperty.Register("MyData", typeof(ProxyArgs), typeof(ServerData), new PropertyMetadata(default(ProxyArgs)));

        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            ProxyJson ProxyJson = new ProxyJson(myini.IniReadValue("MyLanucherConfig", "ProxyPath"));
            if (await ProxyJson.Delete(MyData))
                WeakReferenceMessenger.Default.Send(new ProxyEvnetArgs() { Proxy = MyData, Stuate = XmlProxy.Remove });
        }
    }
}
