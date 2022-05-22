
using GenshinImpact_Lanucher.EventArgs;
using GenshinImpact_Lanucher.Model;
using GenshinImpact_Lanucher.Models;
using GenshinImpact_Lanucher.UserControls;
using GenshinImpact_Lanucher.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinImpact_Lanucher.ViewModels
{
    public class DefaultServerVM: ObservableRecipient, IRecipient<ProxyEvnetArgs>,IRecipient<ServerStuatePorxy>
    {
        public DefaultServerVM()
        {
            IsActive = true;
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
            var cfgpath = myini.IniReadValue("MyLanucherConfig", "ProxyPath");
            //if (File.Exists)
            ProxyJson xml = new ProxyJson(cfgpath);
            AddServer = new RelayCommand(() =>
            {
                var add = new AddServer();
                add.ShowDialog();
            });
            Loaded = new RelayCommand(() =>
            {
                _Lists = xml.ServerProfiles;
            });
            Unloaded = new RelayCommand(() =>
            {
                xml.SaveProfiles();
            });
            CloseProxy = new RelayCommand(() =>
            {
                //在本体发送消息，就直接调用方法
                Receive(new ServerStuatePorxy() { State = ServerStuate.Stop, Message = "关闭服务器", Proxy = null });
            });
        }
        ProxyController Proxy { get; set; }
        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Launcher_Ini myini { get; set; }

        private ObservableCollection<ProxyArgs> Lists;

        public ObservableCollection<ProxyArgs> _Lists
        {
            get => Lists;
            set => SetProperty(ref Lists, value);
        }


        public RelayCommand AddServer { get; set; }
        public RelayCommand Loaded { get; set; }
        public RelayCommand Unloaded { get; set; }

        private bool DialogShow;

        public bool _DialogShow
        {
            get => DialogShow;
            set => SetProperty(ref DialogShow, value);
        }


        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="message">消息</param>
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


        public RelayCommand CloseProxy { get; set; }

        /// <summary>
        /// 服务器状态
        /// </summary>
        /// <param name="message">服务器状态</param>
        public async void Receive(ServerStuatePorxy message)
        {
            switch (message.State)
            {
                case ServerStuate.Runing:
                    {
                        //这里重启一下服务器
                        Proxy = new ProxyController(myini.IniReadValue("MyLanucherConfig", "Port"), message.Proxy.Host);
                        if(await MyHttpClient.GetJson($@"https://{message.Proxy.Host}/status/server") != null)
                        {
                            Proxy.Start();
                            _DialogShow = true;
                           TipWindow.Show("代理启动成功", "如果无法连接请联系服务器管理人员", WPFUI.Common.SymbolRegular.CalendarMail16);
                            break;
                        };
                        TipWindow.Show("服务器无法连接", "请刷新服务器是否可用", WPFUI.Common.SymbolRegular.ErrorCircle24);
                        break;
                    }
                case ServerStuate.Stop:
                    {
                        //服务器停滞状态
                        _DialogShow = false;
                        if (Proxy != null)
                            Proxy.Stop();
                        Proxy = null;
                        break;
                    }
                case ServerStuate.Pause:
                    {
                        //服务器未知状态
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
