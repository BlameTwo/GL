using GL.WinUI3.Model;
using MyApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.WinUI3.Models
{
    public class StartConfigJson
    {
        public StartAgument Config { get; set; } 
        public ObservableCollection<ExeConfig> ExeList { get; set; }
    }


}
