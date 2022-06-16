using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.WinUI3.Model
{
    /// <summary>
    /// 启动器的注册表方法
    /// </summary>
    public class  LanucherRegistryKey
    {
        /// <summary>
        /// 获取游戏目录，是静态方法
        /// </summary>
        /// <returns></returns>
        public static string GetGamePath()
        {
            string startpath = "";
            string launcherpath = GetLauncherPath();
            #region 获取游戏启动路径，和官方配置一致
            if (File.Exists(launcherpath) || File.Exists(launcherpath + "\\config.ini"))
            {
                //获取游戏本体路径
                using (StreamReader reader = new StreamReader(launcherpath + "\\config.ini"))
                {
                    string[] abc = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    foreach (var item in abc)
                    {
                        //从官方获取更多配置
                        if (item.IndexOf("game_install_path") != -1)
                        {
                            startpath += item.Substring(item.IndexOf("=") + 1);
                        }
                    }
                }
            }
            byte[] bytearr = Encoding.UTF8.GetBytes(startpath);
            string path = Encoding.UTF8.GetString(bytearr);
            return path; 
            #endregion
        }


        /// <summary>
        /// 启动器地址
        /// </summary>
        /// <returns></returns>
        public static string GetLauncherPath()
        {
            RegistryKey key = Registry.LocalMachine;            //打开指定注册表根
            //获取官方启动器路径
            string launcherpath = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\原神").GetValue("InstallPath").ToString();
            byte[] bytepath = Encoding.UTF8.GetBytes(launcherpath);     //编码转换
            string path = Encoding.UTF8.GetString(bytepath);
            return path;
            
        }
    }
}
