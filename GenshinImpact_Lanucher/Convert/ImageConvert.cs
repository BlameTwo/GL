using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GenshinImpact_Lanucher.Convert
{
    internal class ImageConvert : IValueConverter
    {
        public static int Number = 800;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new System.Uri(str);
            image.EndInit();
            image.DecodePixelWidth = Number;
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
