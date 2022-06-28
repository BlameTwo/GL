
using GL.WinUI3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUI3.FPS;
using static GL.WinUI3.Model.Launcher_Ini;

namespace GL.WinUI3.Model
{
    public class StartAgument
    {
        /// <summary>
        /// 游戏高度
        /// </summary>
        public string GameHeight { get; set; }

        /// <summary>
        /// 是否无边框
        /// </summary>
        public bool IsPop { get; set; }

        /// <summary>
        /// 解锁帧率
        /// </summary>
        public bool IsFPS { get; set; }

        /// <summary>
        /// 游戏宽度
        /// </summary>
        public string GameWidth { get; set; }

        /// <summary>
        /// 游戏区服
        /// </summary>
        public string GameServer { get; set; }

        /// <summary>
        /// 游戏路径
        /// </summary>
        public string GamePath { get; set; }

        /// <summary>
        /// 是否全屏
        /// </summary>
        public bool full { get; set; }


    }

    /// <summary>
    /// 启动核心
    /// </summary>
    public class StartGame
    {
        public async Task<string> GO(StartConfigJson args,Action action)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    if(args.Config.GameServer == "B服")
                    {
                        Resource.GameIni.GameLauncherWrite("B站");
                        Resource.myini.GameLauncherWrite("B站");
                        GL.WinUI3.Model.Resource.BilibiliSDK(true);
                    }
                    else if(args.Config.GameServer == "官服")
                    {
                        Resource.GameIni.GameLauncherWrite("官服");
                        Resource.myini.GameLauncherWrite("官服");
                        GL.WinUI3.Model.Resource.BilibiliSDK(false);
                    }

                    using (Process p = new Process())
                    {
                        string pop = "";
                        if(args.Config.IsPop == true)
                        {
                            pop = "-popupwindow 1";
                        }
                        if(File.Exists(Path.Combine(args.Config.GamePath , "YuanShen.exe"))){
                            p.StartInfo = new ProcessStartInfo()
                            {
                                FileName =Path.Combine( args.Config.GamePath , "YuanShen.exe"),
                                Verb = "runas",
                                Arguments = $"-screen-fullscreen {System.Convert.ToInt32(args.Config.full)} -screen-height {args.Config.GameHeight}" +
                                $" -screen-width {args.Config.GameWidth} {pop}",
                                WorkingDirectory = args.Config.GamePath,
                                UseShellExecute = true,
                            };

                        }
                        else
                        {
                            p.StartInfo = new ProcessStartInfo()
                            {
                                FileName =Path.Combine( args.Config.GamePath ,"GenshinImpact.exe"),
                                Verb = "runas",
                                Arguments = $"-screen-fullscreen {System.Convert.ToInt32(args.Config.full)} -screen-height {args.Config.GameHeight}" +
                                $" -screen-width {args.Config.GameWidth} {pop}",
                                WorkingDirectory = args.Config.GamePath,
                                UseShellExecute = true,
                            };

                        }
                        if(args.Config.IsFPS == true)
                        {
                            Unlocker unlocker = new(p, 144);
                            p.Start();
                            var result = await unlocker.StartProcessAndUnlockAsync();
                            action.Invoke();
                            
                            return "1";
                        }
                        else
                        {
                            p.Start();
                        }
                        action.Invoke();
                        return "1";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            });
        }

        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    }
}
