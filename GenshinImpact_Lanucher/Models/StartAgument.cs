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




        /// <summary>
        /// 游戏宽度
        /// </summary>
        public string GameWidth { get; set; }

        public Server GameServer { get; set; }


        /// <summary>
        /// 是否全屏
        /// </summary>
        public bool full { get; set; }

        /// <summary>
        /// 窗口无边框化
        /// </summary>
        public string pop { get; set; }

        public static StartAgument GetDefultAgument()
        {
            var start = new StartAgument()
            {
                full =true,
                GameServer=Server.官服,
                GameHeight="1080",
                GameWidth="1980",
                pop = "0"
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
            await Task.Run(() =>
            {
                try
                {
                    using (Process p = new Process())
                    {
                        p.StartInfo = new ProcessStartInfo()
                        {
                            FileName = GenshinImpact_Lanucher.Model.LanucherRegistryKey.GetGamePath() + "//YuanShen.exe",
                            Verb = "runas",
                            Arguments = $"-screen-fullscreen {Convert.ToInt32(args.full)} -screen-height {args.GameHeight}" +
                            $" -screen-width {args.GameWidth} -pop{args.pop}",
                            WorkingDirectory = GenshinImpact_Lanucher.Model.LanucherRegistryKey.GetGamePath(),
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
            return "0";
        }
    }
}
