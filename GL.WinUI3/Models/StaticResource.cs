using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.WinUI3.Models
{
    public class StaticResource
    {
        public readonly static string MyDoc= Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+ @"\GSIConfig";
    }
}
