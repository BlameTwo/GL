using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using static GL.WinUI3.Model.Launcher_Ini;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Media;
using MyApp1.Properties;

namespace GL.WinUI3.Model
{
    public class Resource
    {
        /// <summary>
        /// 从地址中获取图片数据，转换为可显示的图片格式
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ImageSource GetImage(string path)
        {
            var a =  new Microsoft.UI.Xaml.Media.Imaging.BitmapImage(new Uri(path));
            return a;
        }

        static  Launcher_Ini myini { get; set; }
        public static string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        /// <summary>
        /// 修复SDK
        /// </summary>
        /// <param name="flage">是否删除</param>
        /// <returns></returns>
        public static  bool BilibiliSDK(bool flage)
        {
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
            string path = myini.IniReadValue("MyLanucherConfig", "GamePath");
            if (path == null || path.Equals("")) //检测路径是否为空，防止出现崩溃
            {
                return false;
            }
            bool fileexis = File.Exists(path + @"\YuanShen_Data\Plugins\PCGameSDK.dll");
            if (flage == true && fileexis == false)
            {
                string sdk = "PCGameSDK.dll";
                FileStream fs = new FileStream(sdk, FileMode.Create, FileAccess.ReadWrite);
                byte[] buffer = Resources.PCGameSDK;
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
            else if(flage == false && fileexis == true)
            {
                File.Delete(path + @"\YuanShen_Data\Plugins\PCGameSDK.dll");
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
