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


        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public bool WriteMyLauncherConfig(int height)
        {
            try
            {
                IniWriteValue("MyLanucherConfig", "Height", height.ToString());
                Launcher_Ini  myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
                IniWriteValue("MyLanucherConfig", "GamePath", myini.IniReadValue("MyLanucherConfig","GamePath"));
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
                Launcher_Ini myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
                IniWriteValue("MyLanucherConfig", "GamePath", myini.IniReadValue("MyLanucherConfig", "GamePath"));
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
                Launcher_Ini myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
                IniWriteValue("MyLanucherConfig", "GamePath", myini.IniReadValue("MyLanucherConfig", "GamePath"));
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
                }
                else if (Server == Server.官服)
                {
                    IniWriteValue("MyLanucherConfig", "cps", "pcadbdpz");
                    IniWriteValue("MyLanucherConfig", "channel", "1");
                    IniWriteValue("MyLanucherConfig", "sub_channel", "1");
                }
                
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
            }
            else
            {
                agument.full = false;
            }


            if (IniReadValue("MyLauncherConfig", "IsPop") == "True")
            {
                agument.IsPop = true;
            }
            else
            {
                agument.IsPop = false;
            }
           
            if (IniReadValue("General", "channel") =="14" )
            {
                agument.GameServer = Server.B站;
            }
            else
            {
                agument.GameServer = Server.官服;
            }

            agument.GamePath = IniReadValue("MyLanucherConfig", "GamePath");

            return agument;
        }

        public SettingArgs GetSettingArgs()
        {
            SettingArgs args = new SettingArgs();
            args.IsFul = System.Convert.ToBoolean(IniReadValue("MyLanucherConfig", "IsFull"));
            args.GameHeight = int.Parse(IniReadValue("MyLanucherConfig", "Height"));
            args.GameWidth = int.Parse(IniReadValue("MyLanucherConfig", "Width"));
            
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
                WriteMyLauncherConfig(setting.GameHeight, setting.GameWidth, setting.IsFul, setting.server);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 写入服务器配置
        /// </summary>
        /// <param name="IP">IP地址</param>
        /// <param name="Host">端口号</param>
        /// <param name="ServerPath">本地游戏文件夹</param>
        /// <returns></returns>
        public bool WriteServer(string IP,string Host,string ServerPath)
        {
            IniWriteValue("Server", "IP", IP);
            IniWriteValue("Server", "Host",Host);
            IniWriteValue("Server","LocalHost", ServerPath);
            return true;
        }

        public  bool GameLauncherWrite(Server server)
        {
            if (server == Server.B站)        //为B服
            {
                if (Resource.BilibiliSDK() == true)
                {
                    WritePrivateProfileString("General", "cps", "bilibili", LauncherPath);
                    WritePrivateProfileString("General", "sub_channel", "0", LauncherPath);
                    WritePrivateProfileString("General", "channel", "14", LauncherPath);
                    return true;
                }
                return false;
            }
            else if (server == Server.官服)            //严谨一点好
            {
                WritePrivateProfileString("General", "cps", "pcadbdpz", LauncherPath);
                WritePrivateProfileString("General", "sub_channel", "1", LauncherPath);
                WritePrivateProfileString("General", "channel", "1", LauncherPath);
                return true;
            }
            return false;
        }



        
    }
}
