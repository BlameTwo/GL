﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static GenshinImpact_Lanuncher.Model.Launcher_Ini;

namespace GenshinImpact_Lanuncher.Convert
{
    public class MicaConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(System.Convert.ToBoolean(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(System.Convert.ToBoolean(value));
        }
    }

    public class ServerConvert2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Server server = (Server)Enum.Parse(typeof(Server), value.ToString());
            if (server == Server.B站)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Server.官服;
        }
    }
}
