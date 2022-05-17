using GenshinImpact_Lanucher.GameNotifys;
using GenshinImpact_Lanucher.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinImpact_Lanucher.EventArgs
{
    public class Models
    {
    }

    public class ProxyEvnetArgs
    {
        public ProxyArgs Proxy { get; set; }
        public XmlProxy Stuate { get; set; }
    }

    public enum XmlProxy
    {
        Add,Remove,Updata
    }

    public enum ServerStuate
    {
        Runing,Stop,Pause
    }

    public class ServerStuatePorxy
    {
        public ProxyArgs Proxy { get; set; }
        public ServerStuate State { get; set; }
        public string Message { get; set; }
    }


    public class NotiArgs
    {
        public Notice _Notice { get; set; }
    }
}
