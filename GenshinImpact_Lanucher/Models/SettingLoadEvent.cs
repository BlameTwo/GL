
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GenshinImpact_Lanucher.Model.Launcher_Ini;

namespace GameLauncherPrism.Event
{

    
    

    public class SettingArgs
    {
        /// <summary>
        /// 背景模糊度
        /// </summary>
        public double tran { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string Pic_Path { get; set; }
        /// <summary>
        /// 每日一句
        /// </summary>
        public bool OneDay { get; set; }

        /// <summary>
        /// 区服设置
        /// </summary>
        public Server server { get; set; }

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
        public static SettingArgs DefaultSettingArgs()
        {
            SettingArgs args = new SettingArgs();
            args.GameHeight = 768;
            args.GameWidth = 1024;
            args.IsFul = false;
            args.IsPop = true;
            args.OneDay = true;
            args.Pic_Path = "";
            args.tran = 15;
            args.server = Server.官服;
            return args;
        }
    }
}
