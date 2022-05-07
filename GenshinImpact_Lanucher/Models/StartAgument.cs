using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GenshinImpact_Lanucher.Model.Launcher_Ini;

namespace GenshinImpact_Lanucher.Model
{
    public class StartAgument
    {
        /// <summary>
        /// 游戏高度
        /// </summary>
        public string GameHeight { get; set; }

        public bool IsPop { get; set; }

        /// <summary>
        /// 游戏宽度
        /// </summary>
        public string GameWidth { get; set; }

        public Server GameServer { get; set; }

        public string GamePath { get; set; }

        /// <summary>
        /// 是否全屏
        /// </summary>
        public bool full { get; set; }


        public static StartAgument GetDefultAgument()
        {
            var start = new StartAgument()
            {
                full = true,
                GameServer = Server.官服,
                GameHeight = "1080",
                GameWidth = "1980",
                IsPop = false
                , 
                GamePath = LanucherRegistryKey.GetGamePath()
            };
            return start;
        }


    }

    /// <summary>
    /// 启动核心
    /// </summary>
    public class StartGame
    {
        public async Task<string> GO(StartAgument args)
        {
            return await Task.Run(() =>
            {
                try
                {
                    
                    using (Process p = new Process())
                    {
                        string pop = "";
                        if(args.IsPop == true)
                        {
                            pop = "-popupwindow 1";
                        }
                        p.StartInfo = new ProcessStartInfo()
                        {
                            FileName = args.GamePath + "//YuanShen.exe",
                            Verb = "runas",
                            Arguments = $"-screen-fullscreen {System.Convert.ToInt32(args.full)} -screen-height {args.GameHeight}" +
                            $" -screen-width {args.GameWidth} {pop}",
                            WorkingDirectory = args.GamePath,
                            UseShellExecute = true,
                        };
                        p.Start();
                        return "1";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            });
        }
    }
}
