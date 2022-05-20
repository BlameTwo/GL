using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using static GenshinImpact_Lanucher.Model.Launcher_Ini;

namespace GenshinImpact_Lanucher.Model
{
    public class Resource
    {
        /// <summary>
        /// 从地址中获取图片数据，转换为可显示的图片格式
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public BitmapImage GetImage(string path)
        {
            FileStream fs = new System.IO.FileStream(path, FileMode.Open, System.IO.FileAccess.Read);
            byte[] buffer = new byte[fs.Length]; fs.Read(buffer, 0, buffer.Length);
            fs.Close(); fs.Dispose();
            MemoryStream ms = new MemoryStream(buffer);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad; bitmapImage.EndInit();
            ms.Dispose();
            return bitmapImage;
        }

        static  Launcher_Ini myini { get; set; }
        public static string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        /// <summary>
        /// 修复SDK，前提是在本启动器的方法下拥有该文件
        /// </summary>
        /// <returns></returns>
        public static  bool BilibiliSDK()
        {
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
            string path = myini.IniReadValue("MyLanucherConfig", "GamePath");
            if (path == null || path.Equals("")) //检测路径是否为空，防止出现崩溃
            {
                return false;
            }
            if (!File.Exists(path + @"\YuanShen_Data\Plugins\PCGameSDK.dll"))
            {
                string sdk = "PCGameSDK.dll";
                FileStream fs = new FileStream(sdk, FileMode.Create, FileAccess.ReadWrite);
                byte[] buffer = Properties.Resources.PCGameSDK;
                fs.Write(buffer, 0, buffer.Length);
                fs.Close();
                fs.Dispose();
                File.Move(sdk, path + @"\YuanShen_Data\Plugins\PCGameSDK.dll");
                if (File.Exists("PCGameSDK.dll"))
                {
                    File.Delete("PCGameSDK.dll");
                    return true;
                }
            }
            return true;
        }



        /// <summary>
        /// 将程序设置保存在官方ini下
        /// </summary>
        /// <returns></returns>
        public async static Task<bool> ConfigSave(Server server)
        {
            return await Task.Run(() =>
            {
                //游戏ini配置地址
                Launcher_Ini ini = new Launcher_Ini(LanucherRegistryKey.GetGamePath() + "config.ini");
                ini.GameLauncherWrite(server);
                return true;
            });
        }
    }
}
