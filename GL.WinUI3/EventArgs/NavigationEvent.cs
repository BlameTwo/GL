using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp1.EventArgs
{
    public class NavigationEvent
    {
        public Navigations NavigationPage { get; set; }
        public string Message { get; set; } = "";
    }

    public enum Navigations
    {
        Play,
        Server,
        Notify,
        MHY,
        Setting
    }
}
