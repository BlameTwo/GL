using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinImpact_Lanucher.MiHaYouAPI
{
    /// <summary>
    /// 米游社账号信息
    /// </summary>
    public class MiHaYouAccountArgs
    {
        public string AccountImage { get; set; }
        public string AccountName { get; set; }
        public string Uid { get; set; }
    }

    /// <summary>
    /// 原神账号信息
    /// </summary>
    public class GenshinAccountArgs
    {
        public string Name { get; set; }
        public string OwnerServer { get; set; }
        public string Level { get; set; }
        public string Uid { get; set; }
        public string ServerName { get; set; }
    }

    public class GenshinDayArgs
    {
        /// <summary>
        /// 树脂
        /// </summary>
        public string Current_resion { get; set; } = "0";
        /// <summary>
        /// 最大树脂
        /// </summary>
        public string max_resion { get; set; } = "160";
        /// <summary>
        /// 每日委托完成度
        /// </summary>
        public string resin_Day { get; set; } = "0";
        /// <summary>
        /// 最大每日委托
        /// </summary>
        public string max_resion_day { get; set; } = "4";

        /// <summary>
        /// 周本
        /// </summary>
        public string boss { get; set; } = "0";

        /// <summary>
        /// 周本数量
        /// </summary>
        public string max_boss { get; set; } = "3";

        /// <summary>
        /// 洞天宝钱
        /// </summary>
        public string home_money { get; set; } = "0";

        /// <summary>
        /// 每日委托
        /// </summary>
        public ObservableCollection<DayTask> Days { get; set; }

        /// <summary>
        /// 最大洞天宝钱
        /// </summary>
        public string max_home_money { get; set; } = "1600";

        /// <summary>
        /// 质变仪状态
        /// </summary>
        public bool transformer { get; set; } = false;

        /// <summary>
        /// 是否已经获得质变仪
        /// </summary>
        public bool truetransoformer { get; set; } = false;

        /// <summary>
        /// 剩余可用时间
        /// </summary>
        public long transformertime { get; set; } = 0;
    }

    public class DayTask
    {
        /// <summary>
        /// 人物图标
        /// </summary>
        public string IconPath { get; set; }

        /// <summary>
        /// 完成状态
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 预计到什么时候结束
        /// </summary>
        public string end_Time { get; set; }
    }



    public class GenshinMore
    {
        /// <summary>
        /// 活跃天数
        /// </summary>
        public string happyday { get; set; }

        /// <summary>
        /// 成就达成数
        /// </summary>
        public string gamecount { get; set; }
        /// <summary>
        /// 人物数量
        /// </summary>
        public string peoplecount { get; set; }

        /// <summary>
        /// 深渊挑战
        /// </summary>
        public string Challengecount { get; set; }

        /// <summary>
        /// 传送点
        /// </summary>
        public string movecount { get; set; }

        /// <summary>
        /// 解锁的秘境
        /// </summary>
        public string bosscopy { get; set; }

        /// <summary>
        /// 华丽宝箱
        /// </summary>
        public string Level1 { get; set; }
        /// <summary>
        /// 珍贵宝箱
        /// </summary>
        public string Level2 { get; set; }
        /// <summary>
        /// 精致宝箱
        /// </summary>
        public string Level3 { get; set; }
        /// <summary>
        /// 普通宝箱
        /// </summary>
        public string Level4 { get; set; }
        /// <summary>
        /// 家具宝箱
        /// </summary>
        public string Last_Level { get; set; }
        /// <summary>
        /// 风神瞳
        /// </summary>
        public string eye1 { get; set; }
        /// <summary>
        /// 岩神瞳
        /// </summary>
        public string eye2 { get; set; }
        /// <summary>
        /// 雷神瞳
        /// </summary>
        public string eye3 { get; set; }

        public ObservableCollection<GenshinWorld> Worlds { get; set; } = new ObservableCollection<GenshinWorld>();

        public ObservableCollection<GenshinHome> Homes { get; set; } = new ObservableCollection<GenshinHome>();

        
    }


    public class GenshinWorld
    {
        /// <summary>
        /// 等级
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 进度
        /// </summary>
        public double Progess { get; set; }

        /// <summary>
        /// 地图图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 背景图片
        /// </summary>
        public string BackImage { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }


        public ObservableCollection<WorldOffire> Offire { get; set; } = new ObservableCollection<WorldOffire>();
    }


    public class WorldOffire
    {
        public string Name { get; set; }
        public string Level { get; set; }
        public string Icon { get; set; }
    }


    public class GenshinHome
    {
        /// <summary>
        /// 等级
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 访问人数
        /// </summary>
        public string peoplecount { get; set; }

        /// <summary>
        /// 最高仙力
        /// </summary>
        public string power { get; set; }

        /// <summary>
        /// 摆件数量
        /// </summary>
        public string bluecount { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 背景图片
        /// </summary>
        public string BackImage { get; set; }

        /// <summary>
        /// 文字等级
        /// </summary>
        public string StringLevel { get; set; }
    }
    
}
