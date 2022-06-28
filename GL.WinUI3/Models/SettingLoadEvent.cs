
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GL.WinUI3.Model.Launcher_Ini;

namespace GameLauncherPrism.Event
{

    
    

    public class SettingArgs
    {
        /// <summary>
        /// 区服设置,true为官服，false为B服
        /// </summary>
        public string server { get; set; }

        /// <summary>
        /// 游戏宽度
        /// </summary>
        public int GameWidth { get; set; }

        /// <summary>
        /// 游戏高度
        /// </summary>
        public int GameHeight { get; set; }

        /// <summary>
        /// 是否全屏
        /// </summary>
        public bool IsFul { get; set; }

        //是否无边框
        public bool IsPop { get; set; }

        public string JarPath { get; set; }

        public string ServerPath { get; set; }

        public static SettingArgs DefaultSettingArgs()
        {
            SettingArgs args = new SettingArgs();
            args.GameHeight = 768;
            args.GameWidth = 1024;
            args.IsFul = false;
            args.IsPop = true;
            args.server = "官服";
            
            return args;
        }
    }
}
