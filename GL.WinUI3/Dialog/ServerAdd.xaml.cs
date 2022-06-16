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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyApp1.Dialog
{
    public sealed partial class ServerAdd : ContentDialog
    {
        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Launcher_Ini myini { get; set; }
        public ServerAdd()
        {
            this.InitializeComponent(); 
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
        }

        private async  void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if(string.IsNullOrWhiteSpace(ServerIP.Text) || string.IsNullOrWhiteSpace(ServerName.Text))
            {
                TipText.Text = "请输入正确的IP和名称！";
                args.Cancel = true;
            }
            else
            {
                TipText.Text = "";
                ProxyArgs arg = new ProxyArgs()
                {
                    Name = ServerName.Text,
                    Host = ServerIP.Text
                };
                ProxyJson xml = new ProxyJson(myini.IniReadValue("MyLanucherConfig", "ProxyPath"));
                if (await xml.Add(arg))
                {
                    ProxyEvnetArgs arg1 = new ProxyEvnetArgs();
                    arg1.Proxy = arg;
                    arg1.Stuate = XmlProxy.Add;
                    WeakReferenceMessenger.Default.Send(arg1);
                    this.Hide();
                };
            }

        }
    }
}
