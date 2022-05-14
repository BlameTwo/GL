using GenshinImpact_Lanucher.EventArgs;
using GenshinImpact_Lanucher.Model;
using GenshinImpact_Lanucher.UserControls;
using GenshinImpact_Lanucher.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            ProxyJson xml = new ProxyJson(myini.IniReadValue("MyLanucherConfig", "ProxyPath"));
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
        }

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
            if(message.Stuate == XmlProxy.Remove)
            {
                _Lists.Remove(message.Proxy);
            }
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


        /// <summary>
        /// 服务器状态
        /// </summary>
        /// <param name="message">服务器状态</param>
        public void Receive(ServerStuatePorxy message)
        {
            switch (message.State)
            {
                case ServerStuate.Runing:
                    {
                        _DialogShow = true;
                        break;
                    }
                case ServerStuate.Stop:
                    {
                        //服务器停滞状态
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
