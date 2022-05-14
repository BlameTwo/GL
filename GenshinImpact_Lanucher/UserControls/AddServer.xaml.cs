using GenshinImpact_Lanucher.EventArgs;
using GenshinImpact_Lanucher.Model;
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
using WPFUI.Appearance;
using WPFUI.Controls;

namespace GenshinImpact_Lanucher.UserControls
{
    /// <summary>
    /// AddServer.xaml 的交互逻辑
    /// </summary>
    public partial class AddServer : WPFUI.Controls.MessageBox
    {
        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Launcher_Ini myini { get; set; }
        public AddServer()
        {
            InitializeComponent();
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async  void Button_Click(object sender, RoutedEventArgs e)
        {
            ProxyArgs args = new ProxyArgs()
            {
                Name = NameText.Text,
                Host = IpText.Text
            };
            ProxyXml xml = new ProxyXml ( myini.IniReadValue("MyLanucherConfig", "ProxyPath"));
            if(await xml.Add(args))
            {
                ProxyEvnetArgs arg = new ProxyEvnetArgs();
                arg.Proxy = args;
                arg.Stuate = XmlProxy.Add;
                WeakReferenceMessenger.Default.Send(arg);
                this.Close();
            };
        }
    }
}
