
using GenshinImpact_Lanucher.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GenshinImpact_Lanucher.GameNotifys
{
    public static class GameNitify
    {
        public const string MoreUrl = "https://hk4e-api.mihoyo.com/common/hk4e_cn/announcement/api/getAnnContent?game=hk4e&game_biz=hk4e_cn&lang=zh-cn&bundle_id=hk4e_cn&platform=pc&region=cn_gf01&level=55&uid=100000000";
        public const string Url = "https://hk4e-api.mihoyo.com/common/hk4e_cn/announcement/api/getAnnList?game=hk4e&game_biz=hk4e_cn&lang=zh-cn&bundle_id=hk4e_cn&platform=pc&region=cn_gf01&level=55&uid=100000000";
        /// <summary>
        /// 获取原神公告
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        static string GetTips(int type)
        {
            string retString = MyHttpClient.GetJson(Url);
            var joject = JObject.Parse(retString);
            string message =  joject["message"].ToString();
            if (message=="OK")
            {
                var data = joject["data"]["list"];
                List<string> list = new List<string>();
                foreach (var item in data)
                {
                    list.Add(item.ToString());
                }
                if (type == 0)
                {
                    return list[0];
                    
                }
                return list[1];
            }
            else
            {
                return null;
            }
        }

        public static async Task<ObservableCollection<Notice>> GetOneAsync()
        {
            return await Task.Run(GetOne);
        }

        

        /// <summary>
        /// 此方法返回活动公告
        /// </summary>
        /// <returns></returns>
        public async static Task<ObservableCollection<Notice>> GetOne()
        {
            return await Task.Run(() =>
            {
                var list = new ObservableCollection<Notice>();
                JObject jo = JObject.Parse(GetTips(0));
                JArray jas = (JArray)jo["list"];
                foreach (JToken ja in jas)
                {
                    list.Add(InitNotice(ja));
                }
                return list;
            });
        }


        public static async Task<ObservableCollection<Notice>> GetTwoAsync()
        {

            return await Task.Run(GetTwo);
        }

        static Notice  InitNotice(JToken ja)
        {
            Notice no = new Notice()
            {
                ann_id = ja["ann_id"].ToString(),
                title = ja["title"].ToString(),
                subtitle = ja["subtitle"].ToString(),
                banner = ja["banner"].ToString(),
                content = ja["content"].ToString(),
                type_label = ja["type_label"].ToString(),
                tag_label = ja["tag_label"].ToString(),
                login_alert = ja["login_alert"].ToString(),
                lang = ja["lang"].ToString(),
                Start_time = ja["start_time"].ToString(),
                End_time = ja["end_time"].ToString(),
                type = ja["type"].ToString(),
                remind = ja["remind"].ToString(),
                alert = ja["alert"].ToString(),
                tag_start_time = ja["tag_start_time"].ToString(),
                tag_end_time = ja["tag_end_time"].ToString(),
                remind_ver = ja["remind_ver"].ToString(),
                has_content = ja["has_content"].ToString()
            };
            return no;
        }

        /// <summary>
        /// 此方法返回游戏公告
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Notice> GetTwo()
        {
            var list = new ObservableCollection<Notice>();
            JObject jo = JObject.Parse(GetTips(1));
            JArray jas = (JArray)jo["list"];
            foreach (JToken ja in jas)
            {
                list.Add(InitNotice(ja));
            }
            return list;
        }
    }


    public class Notice
    {
        public string title { get; set; }
        public string ann_id { get; set; }
        public string subtitle { get; set; }
        public string banner { get; set; }
        public string content { get; set; }
        public string type_label { get; set; }
        public string Start_time { get; set; }
        public string End_time { get; set; }
        public string login_alert { get; set; }
        public string tag_label { get; set; }
        public string lang { get; set; }
        public string type { get; set; }
        public string remind { get; set; }
        public string alert { get; set; }
        public string tag_start_time { get; set; }
        public string tag_end_time { get; set; }
        public string Tag_Icon { get; set; }
        public string remind_ver { get; set; }
        public string has_content { get; set; }
    }
}
