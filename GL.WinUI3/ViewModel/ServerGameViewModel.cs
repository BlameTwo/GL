using GenshinImpact_Lanuncher.Utils;
using GL.WinUI3;
using GL.WinUI3.EventArgs;
using GL.WinUI3.Model;
using GL.WinUI3.Models;
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
    public class ServerGameViewModel: ObservableRecipient, IRecipient<ProxyEvnetArgs>, IRecipient<ServerStuatePorxy>
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
            StopProxy = new RelayCommand(() =>
            {
                Receive(new ServerStuatePorxy() { State = ServerStuate.Stop, Message = "关闭服务器", Proxy = null });
            });
            _ItemsEnable = true;
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

        public async void Receive(ServerStuatePorxy message)
        {
            switch (message.State)
            {
                case ServerStuate.Runing:
                    {
                        if (await MyHttpClient.GetJson($@"https://{message.Proxy.Host}/status/server") != null)
                        {
                            _ItemsEnable = false;
                            System.Diagnostics.Process p = new System.Diagnostics.Process();
                            p.StartInfo = new System.Diagnostics.ProcessStartInfo()
                            {
                                UseShellExecute = false ,
                                FileName = $@"{docpath}\GSIConfig\Proxy\ProxyHelper.exe",
                                CreateNoWindow = false,
                            }; 
                            p.StartInfo.RedirectStandardInput = true;
                            p.StartInfo.RedirectStandardOutput = true;
                            p.Start();
                            p.StandardInput.WriteLine($@"start{message.Proxy.Host}");
                            (App.MainWindow as MainWin).Title = "服务器连接成功";
                            var output = p.StandardOutput.ReadToEnd();
                            break;
                        };
                        (App.MainWindow as MainWin).Title = "服务器连接失败";
                        break;
                    }
                case ServerStuate.Stop:
                    {
                        _ItemsEnable = true;
                        break;
                    }
                case ServerStuate.Pause:
                    {
                        (App.MainWindow as MainWin).Title = "服务器状态不知";
                        //服务器未知状态
                        break;
                    }
                default:
                    break;
            }
        }

        private ObservableCollection<ProxyArgs> Lists;

        public ObservableCollection<ProxyArgs> _Lists
        {
            get => Lists;
            set => SetProperty(ref Lists, value);
        }

        private bool ItemsEnable;

        public bool _ItemsEnable
        {
            get { return ItemsEnable; }
            set => SetProperty(ref ItemsEnable, value);
        }


        public RelayCommand Serveradd { get; private set; }

        public RelayCommand StopProxy { get; private set; }


    }
}
