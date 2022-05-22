
using GenshinImpact_Lanucher.EventArgs;
using GenshinImpact_Lanucher.Model;
using GenshinImpact_Lanucher.Models;
using GenshinImpact_Lanucher.Utils;
using Microsoft.Toolkit.Mvvm.Messaging;
using Newtonsoft.Json.Linq;
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
    /// ServerDt.xaml 的交互逻辑
    /// </summary>
    public partial class ServerDT : UserControl
    {
        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Launcher_Ini myini { get; set; }
        public ServerDT()
        {
            InitializeComponent();

            this.myini = new Launcher_Ini($@"{Resource.docpath}/GSIConfig/Config/LauncherConfig.ini");
        }
        public ProxyArgs MyData
        {
            get { return (ProxyArgs)GetValue(MyDataProperty); }
            set { SetValue(MyDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyDataProperty =
            DependencyProperty.Register("MyData", typeof(ProxyArgs), typeof(ServerDT), new PropertyMetadata(null, new PropertyChangedCallback(async (s, e) =>
            {
                var objectelment = s as ServerDT;
                var value = e.NewValue as ProxyArgs;
                await Refallt(objectelment, value);
            })));


        async static Task<bool> Refallt(ServerDT dt, ProxyArgs args)
        {
            string json = await MyHttpClient.GetJson($@"https://{args.Host}/status/server");
            if (json != null)
            {
                JObject jo = JObject.Parse(json);
                dt.PeopleCount.Text = jo["status"]["playerCount"].ToString();
                dt.ServerVersion.Text = jo["status"]["version"].ToString();
                dt.StateServer.Foreground = new SolidColorBrush(Colors.Green);
                return true;
            }
            return false;
        }

        private async void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ProxyJson ProxyJson = new ProxyJson(myini.IniReadValue("MyLanucherConfig", "ProxyPath"));
            if (await ProxyJson.Delete(MyData))
                WeakReferenceMessenger.Default.Send(new ProxyEvnetArgs() { Proxy = MyData, Stuate = XmlProxy.Remove });
            //else
            //    WindowTip.TipShow("错误！请联系制作人员！", "此错误来自于逻辑层面错误，必要的话，请把日志文件发给开发者", WPFUI.Common.SymbolRegular.ErrorCircleSettings16);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (await Refallt(this, MyData))
            {
                StateServer.Foreground = new SolidColorBrush(Colors.Green);
                WindowTip.TipShow("状态刷新成功", "服务器状态正常", WPFUI.Common.SymbolRegular.InprivateAccount16);

            }
            else
            {
                this.PeopleCount.Text = "Null";
                this.ServerVersion.Text = "服务器寄了";
                WindowTip.TipShow("无法连接", "无法连接到服务器！", WPFUI.Common.SymbolRegular.ErrorCircle24);
            };

        }
    }
}
