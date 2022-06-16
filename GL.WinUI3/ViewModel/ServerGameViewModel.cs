using GenshinImpact_Lanuncher.Utils;
using GL.WinUI3;
using GL.WinUI3.EventArgs;
using GL.WinUI3.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using MyApp1.Dialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp1.ViewModel
{
    public class ServerGameViewModel: ObservableRecipient, IRecipient<ProxyEvnetArgs>
    {
        public ServerGameViewModel()
        {
            IsActive = true;
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
            Serveradd = new RelayCommand(async () =>
            {
                ServerAdd add = new ServerAdd();
                add.XamlRoot = (App.MainWindow as MainWin).MyFrame.XamlRoot;
                await add.ShowAsync();
            });
        }

        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Launcher_Ini myini { get; set; }
        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var cfgpath = myini.IniReadValue("MyLanucherConfig", "ProxyPath");
            if (!File.Exists(cfgpath))
            {
                //创建
                File.CreateText(docpath+ @"\GSIConfig\Config\Proxy.json").Dispose();
                myini.IniWriteValue("MyLanucherConfig", "ProxyPath", docpath + @"\GSIConfig\Config\Proxy.json");
            }
            ProxyJson xml = new ProxyJson(cfgpath);
            _Lists = xml.ServerProfiles;
        }

        public void Receive(ProxyEvnetArgs message)
        {
            switch (message.Stuate)
            {
                case XmlProxy.Add:
                    _Lists.Add(message.Proxy);
                    break;
                case XmlProxy.Remove:
                    _Lists.Remove(message.Proxy);
                    break;
                case XmlProxy.Updata:
                    //暂时不实现
                    break;
            }
        }

        private ObservableCollection<ProxyArgs> Lists;

        public ObservableCollection<ProxyArgs> _Lists
        {
            get => Lists;
            set => SetProperty(ref Lists, value);
        }


        public RelayCommand Serveradd { get; private set; }



    }
}
