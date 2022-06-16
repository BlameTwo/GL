
using GenshinImpact_Lanuncher.Utils;
using GL.WinUI3.GameNotifys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.WinUI3.EventArgs
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
