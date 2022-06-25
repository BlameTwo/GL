using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp1.WindowHelper
{
    public  class INavigations
    {
        public string Message { get; set; }
        public Action MyAction { get; set; }
    };
    public class NavigationHelper
    {
        public void GO(INavigations Navigation)
        {
            Navigation.MyAction.Invoke();
            Debug.WriteLine($"[{DateTime.Now.ToString()}]:{Navigation.Message}");
        }
    }
}
