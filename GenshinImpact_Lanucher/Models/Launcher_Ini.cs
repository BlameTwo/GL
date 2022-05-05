using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows;
using GameLauncherPrism.Event;

namespace GenshinImpact_Lanucher.Model
{
    public class Launcher_Ini
    {
        // 声明INI文件的写操作函数 WritePrivateProfileString()
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // 声明INI文件的读操作函数 GetPrivateProfileString()
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);
        public string LauncherPath { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">启动器地址</param>
        public Launcher_Ini(string path)
        {
            this.LauncherPath = path;
        }


        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        /// <param name="Value">值</param> 
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.LauncherPath);
        }


        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.LauncherPath);
            return temp.ToString();
        }



        public bool WriteMyLauncherConfig(int height)
        {
            try
            {
                IniWriteValue("MyLanucherConfig", "Height", height.ToString());
                IniWriteValue("MyLanucherConfig", "GamePath", LanucherRegistryKey.GetGamePath());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 写入配置
        /// </summary>
        /// <param name="width">数值，不一定是width</param>
        /// <param name="type">是否是“Width” Or "Height"</param>
        /// <returns></returns>
        public bool WriteMyLauncherConfig(int width,string type)
        {
            try
            {
                if (type == "Width")
                {
                    IniWriteValue("MyLanucherConfig", "Width", width.ToString());
                }
                if (type == "Height")
                {
                    IniWriteValue("MyLanucherConfig", "Height", width.ToString());
                }
                IniWriteValue("MyLanucherConfig", "GamePath", LanucherRegistryKey.GetGamePath());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 写入配置
        /// </summary>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        /// <returns></returns>
        public bool WriteMyLauncherConfig(int height,int width)
        {
            try
            {
                WriteMyLauncherConfig(height);
                IniWriteValue("MyLanucherConfig", "Width", width.ToString());
                IniWriteValue("MyLanucherConfig", "GamePath", LanucherRegistryKey.GetGamePath());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public bool WriteMyLauncherConfig(int height, int width,bool IsFull)
        {
            try
            {
                WriteMyLauncherConfig(height, width);
                IniWriteValue("MyLanucherConfig", "IsFull", IsFull.ToString());
                return true;
            }catch (Exception){return false;}
        }

        /// <summary>
        /// 写入配置
        /// </summary>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        /// <param name="IsFull">是否全屏</param>
        /// <param name="Server">服务器选择，“B站”或“官服”</param>
        /// <returns></returns>
        public bool WriteMyLauncherConfig(int height, int width, bool IsFull,Server Server)
        {
            try
            {
                WriteMyLauncherConfig(height, width, IsFull);
                if (Server == Server.B站)
                {
                    IniWriteValue("MyLanucherConfig", "cps", "bilibili");
                    IniWriteValue("MyLanucherConfig", "channel", "14");
                    IniWriteValue("MyLanucherConfig", "sub_channel", "0");
                    //修复SDK
                    if (Resource.BilibiliSDK())
                        MessageBox.Show("SDK修复完毕");
                }
                else if (Server == Server.官服)
                {
                    IniWriteValue("MyLanucherConfig", "cps", "pcadbdpz");
                    IniWriteValue("MyLanucherConfig", "channel", "1");
                    IniWriteValue("MyLanucherConfig", "sub_channel", "1");
                    IniWriteValue("MyLanucherConfig", "GamePath", LanucherRegistryKey.GetGamePath());
                }
                
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// 写入配置
        /// </summary>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        /// <param name="IsFull">是否全屏</param>
        /// <param name="Server">服务器选择，“B站”或“官服”</param>
        /// <param name="tran">背景透明°</param>
        /// <returns></returns>
        public bool WriteMyLauncherConfig(int height, int width, bool IsFull, Server Server,double tran)
        {
            try
            {
                WriteMyLauncherConfig(height, width, IsFull, Server);
                IniWriteValue("MyLanucherConfig", "tran", tran.ToString());
                return true;
            }catch (Exception){return false;}
        }

        /// <summary>
        /// 写入配置
        /// </summary>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        /// <param name="IsFull">是否全屏</param>
        /// <param name="Server">服务器选择，“B站”或“官服”</param>
        /// <param name="tran">背景透明°</param>
        /// <param name="OneDay">是否显示每日一句</param>
        /// <returns></returns>
        public bool WriteMyLauncherConfig(int height, int width, bool IsFull, Server Server, double tran,bool OneDay)
        {
            try
            {
                WriteMyLauncherConfig(height, width, IsFull, Server,tran);
                IniWriteValue("MyLanucherConfig", "OneDay", OneDay.ToString());
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// 写入配置
        /// </summary>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        /// <param name="IsFull">是否全屏</param>
        /// <param name="Server">服务器选择，“B站”或“官服”</param>
        /// <param name="tran">背景透明°</param>
        /// <param name="OneDay">是否显示每日一句</param>
        /// <param name="PicPath">图片地址</param>
        /// <returns></returns>
        public bool WriteMyLauncherConfig(int height, int width, bool IsFull, Server Server, double tran, bool OneDay,string PicPath)
        {
            try
            {
                WriteMyLauncherConfig(height, width, IsFull, Server, tran,OneDay);
                IniWriteValue("MyLanucherConfig", "PicPath", PicPath.ToString());
                return true;
            }
            catch (Exception) { return false; }
        }


        public bool WriteMyLauncherConfig(int height, int width, bool IsFull, Server Server, double tran, bool OneDay, string PicPath,string JarPath)
        {
            try
            {
                WriteMyLauncherConfig(height, width, IsFull, Server, tran, OneDay);
                IniWriteValue("MyLanucherConfig", "JarPath", JarPath.ToString());
                return true;
            }
            catch (Exception) { return false; }
        }



        public StartAgument GetAgument()
        {
            StartAgument agument = new StartAgument{
                GameHeight = IniReadValue("MyLanucherConfig","Height"),
                GameWidth = IniReadValue("MyLanucherConfig", "Width"),
            };

            if (IniReadValue("MyLanucherConfig", "IsFull") == "True"){
                agument.full = true;
                agument.pop = "-popupwindow";        //这是设置是否无边框化
            }
            else{
                agument.full = false;
            }
            if (IniReadValue("MyLanucherConfig", "channel") =="14" )
            {
                agument.GameServer = Server.B站;
            }
            else
            {
                agument.GameServer = Server.官服;
            }


            return agument;
        }

        public SettingArgs GetSettingArgs()
        {
            SettingArgs args = new SettingArgs();
            args.IsFul = Convert.ToBoolean(IniReadValue("MyLanucherConfig", "IsFull"));
            args.GameHeight = int.Parse(IniReadValue("MyLanucherConfig", "Height"));
            args.GameWidth = int.Parse(IniReadValue("MyLanucherConfig", "Width"));
            args.OneDay = Convert.ToBoolean(IniReadValue("MyLanucherConfig", "OneDay"));
            args.Pic_Path = IniReadValue("MyLanucherConfig", "PicPath");
            args.tran = Convert.ToDouble(IniReadValue("MyLanucherConfig", "tran"));
            if (IniReadValue("MyLanucherConfig", "channel") == "14")
            {
                args.server = Server.B站;
            }
            else
            {
                args.server = Server.官服;
            }
            args.IsPop = args.IsFul?false:true;

            return args;
        }
        public enum Server
        {
            B站,官服
        }



        /// <summary>
        /// 小判断
        /// </summary>
        /// <param name="server1"></param>
        /// <param name="server2"></param>
        /// <returns></returns>
        public static Server GetServer3(bool server1, bool server2)
        {
            if (server1 == true)
            {
                return Server.B站;
            }
            return Server.官服;
        }


        public  bool SaveDefaultSettingArgs()
        {
            try
            {
                var setting = SettingArgs.DefaultSettingArgs();
                WriteMyLauncherConfig(setting.GameHeight, setting.GameWidth, setting.IsFul, setting.server, setting.tran, setting.OneDay, setting.Pic_Path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> GameLauncherWrite(Server server)
        {
            //就这一个改成多线程吧，上面的就不改了
            await Task.Run(() =>
            {
                if (server == Server.B站)        //为B服
                {
                    WritePrivateProfileString("General", "cps", "bilibili", LauncherPath);
                    WritePrivateProfileString("General", "sub_channel", "0", LauncherPath);
                    WritePrivateProfileString("General", "channel", "14", LauncherPath);
                    Resource.BilibiliSDK();
                }
                else if (server == Server.官服)            //严谨一点好
                {
                    WritePrivateProfileString("General", "cps", "pcadbdpz", LauncherPath);
                    WritePrivateProfileString("General", "sub_channel", "1", LauncherPath);
                    WritePrivateProfileString("General", "channel", "1", LauncherPath);
                    return true;
                }
                return false;
            });
            return false;
        }



        
    }
}
