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
    public class DefaultServerVM: ObservableRecipient, IRecipient<ProxyEvnetArgs>
    {
        public DefaultServerVM()
        {
            IsActive = true;
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
            ProxyXml xml = new ProxyXml(myini.IniReadValue("MyLanucherConfig", "ProxyPath"));
            AddServer = new RelayCommand(() =>
            {
                var add = new AddServer();
                add.ShowDialog();
            });
            Loaded = new RelayCommand(async () =>
            {
                _Lists = await xml.ReadValue();
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
    }
}
