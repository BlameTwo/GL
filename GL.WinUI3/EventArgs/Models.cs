
using GenshinImpact_Lanuncher.Utils;
using GL.WinUI3.GameNotifys;
using MyApp1.Models;
using MyApp1.MyControl;
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

    public class ExeArgs
    {
        public ExeConfig Config { get; set; }
        public ExeEnum ExeEnum { get; set; }    
    }

    public enum ExeEnum
    {
        Add,Remove
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

    public class ServerChanged
    {
        public string Host { get; set; }
        public ServerData Data { get; set; }

        public string Message { get; set; }
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
