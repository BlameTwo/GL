using GenshinImpact_Lanucher.EventArgs;
using GenshinImpact_Lanucher.Model;
using GenshinImpact_Lanucher.Models;
using GenshinImpact_Lanucher.Utils;
using Microsoft.Toolkit.Mvvm.Messaging;
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
    public partial class ServerDT: UserControl
    {


        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Launcher_Ini myini { get; set; }
        public ServerDT()
        {
            InitializeComponent();
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
        }






        public ProxyArgs MyData
        {
            get { return (ProxyArgs)GetValue(MyDataProperty); }
            set { SetValue(MyDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyDataProperty =
            DependencyProperty.Register("MyData", typeof(ProxyArgs), typeof(ServerDT), new PropertyMetadata(null));

        private async void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ProxyJson ProxyJson = new ProxyJson(myini.IniReadValue("MyLanucherConfig", "ProxyPath"));
            if (await ProxyJson.Delete(MyData))
                WeakReferenceMessenger.Default.Send(new ProxyEvnetArgs() { Proxy = MyData, Stuate = XmlProxy.Remove });
            else
                WindowTip.TipShow("错误！请联系制作人员！", "此错误来自于逻辑层面错误，必要的话，请把日志文件发给开发者", WPFUI.Common.SymbolRegular.ErrorCircleSettings16);
        }
    }
}
