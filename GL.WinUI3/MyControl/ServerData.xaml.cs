using GenshinImpact_Lanuncher.Utils;
using GL.WinUI3.EventArgs;
using GL.WinUI3.Model;
using GL.WinUI3.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Newtonsoft.Json.Linq;
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

        private async  void Button_Click(object sender, RoutedEventArgs e)
        {
            await Refallt(this, MyData);
        }

        async static Task<bool> Refallt(ServerData dt, ProxyArgs args)
        {
            string json = await MyHttpClient.GetJson($@"https://{args.Host}/status/server");
            if (json != null)
            {
                JObject jo = JObject.Parse(json);
                dt.PeopleCount.Text = jo["status"]["playerCount"].ToString();
                dt.GameVersion.Text = jo["status"]["version"].ToString();
                return true;
            }
            return false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ServerStuatePorxy arg = new ServerStuatePorxy()
            {
                State = ServerStuate.Runing,
                Message = "连接到服务器成功！"
                ,
                Proxy = MyData
            };
            WeakReferenceMessenger.Default.Send(arg);
        }
    }
}
