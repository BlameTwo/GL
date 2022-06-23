using GenshinImpact_Lanuncher.Utils;
using GL.WinUI3;
using GL.WinUI3.EventArgs;
using GL.WinUI3.Model;
using GL.WinUI3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using MyApp1.Dialog;
using MyApp1.Models;
using MyApp1.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI;

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
                add.XamlRoot = ((App.MainWindow as MainWin).Content as MainPage).MyFrame.XamlRoot;
                await add.ShowAsync();
            });
            StopProxy = new RelayCommand(() =>
            {
                Receive(new ServerStuatePorxy() { State = ServerStuate.Stop, Message = "关闭服务器", Proxy = null });
            });
            _ItemsEnable = true;
            PlayGame = new RelayCommand(async () =>
            {
                StartGame startAgument = new StartGame();
                string a = await startAgument.GO(myini.GetAgument());
                if (a == "1")
                {
                    TipWindow.Show("启动游戏成功！", "可以快乐的玩耍了");
                }
                else
                {

                    TipWindow.Show("启动游戏失败", "😒请检查游戏文件夹");
                };
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

        private SolidColorBrush Fill;

        public SolidColorBrush _Fill
        {
            get { return Fill; }
            set => SetProperty(ref Fill, value);
        }

        private string SelectText;

        public string _SelectText
        {
            get { return SelectText; }
            set => SetProperty(ref SelectText, value);
        }



        public  void Receive(ServerStuatePorxy message)
        {
            switch (message.State)
            {
                case ServerStuate.Runing:
                    {
                        _ItemsEnable = false;
                        var win = (App.MainWindow as MainWin);
                        win.Title = "连接服务器成功";
                        _Fill = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                        _SelectText = $"已经连接到{message.Proxy.Name}";
                        break;
                    }
                case ServerStuate.Stop:
                    {
                        _ItemsEnable = true;
                        var win = (App.MainWindow as MainWin);
                        win.Title = "服务器断开连接";
                        _Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                        _SelectText = "连接断开";
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

        public RelayCommand PlayGame { get; private set; }

        public RelayCommand Setup_Book { get; private set; } = new RelayCommand(() =>
        {
            App.helper.RunCMD("setup");
        });

        public RelayCommand UnSetup_Book { get; private set; } = new RelayCommand(() =>
        {
            App.helper.RunCMD("unsetup");
        });
    }
}
