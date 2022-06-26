using GL.UI.Models;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace MyApp1.Converter
{
    internal class IconConvert
    {
        public static async  Task<BitmapImage> Convert(string value)
        {
            try
            {
                Bitmap result = IconHelper.GetIcon(value.ToString());
                var ms = new MemoryStream();
                result.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                using (var stream = new InMemoryRandomAccessStream())
                {
                    using (var writer = new DataWriter(stream.GetOutputStreamAt(0)))
                    {
                        writer.WriteBytes(byteImage);
                        await writer.StoreAsync();
                    }
                    BitmapImage bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(stream);
                    return bitmapImage;
                }
            }
            catch (Exception)
            {
                var bit = new BitmapImage(new Uri("ms-appx:///Assets/app.png"));
                return bit;
            }
            
        }
    }
}
