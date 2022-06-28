using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GL.WinUI3.Model.Launcher_Ini;

namespace GL.WinUI3.Converter
{
    public class ServerConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value.ToString() == "官服")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
