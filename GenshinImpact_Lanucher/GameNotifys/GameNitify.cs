
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
        static async Task<string> GetTips(int type)
        {
            string retString = await MyHttpClient.GetJson(Url);       //简略公告
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
            return await Task.Run(async () =>
            {
                var list = new ObservableCollection<Notice>();

                string morestring = await MyHttpClient.GetJson(MoreUrl);      //完整公告
                JObject jo = JObject.Parse(await GetTips(0));
                JArray morestr = JArray.Parse( JObject.Parse(morestring)["data"]["list"].ToString());
                JArray jas = (JArray)jo["list"];
                foreach (JToken ja in jas)
                {
                    list.Add(InitNotice(ja,ref morestr));
                }
                return list;
            });
        }


        public static async Task<ObservableCollection<Notice>> GetTwoAsync()
        {

            return await Task.Run(GetTwo);
        }

        static Notice  InitNotice(JToken ja,ref JArray morejar)
        {
            Notice no = new Notice();
            no.ann_id = ja["ann_id"].ToString();
            no.title = ja["title"].ToString();
            no.subtitle = ja["subtitle"].ToString();
            no.banner = ja["banner"].ToString();
            no.content = ja["content"].ToString();
            no.type_label = ja["type_label"].ToString();
            no.tag_label = ja["tag_label"].ToString();
            no.login_alert = ja["login_alert"].ToString();
            no.lang = ja["lang"].ToString();
            no.Start_time = ja["start_time"].ToString();
            no.End_time = ja["end_time"].ToString();
            no.type = ja["type"].ToString();
            no.remind = ja["remind"].ToString();
            no.alert = ja["alert"].ToString();
            no.tag_start_time = ja["tag_start_time"].ToString();
            no.tag_end_time = ja["tag_end_time"].ToString();
            no.remind_ver = ja["remind_ver"].ToString();
            no.has_content = ja["has_content"].ToString();
            foreach (var item in morejar)
            {
                string str1 = item["ann_id"].ToString();
                string str2 = ja["ann_id"].ToString();
                if (str1 == str2)           //如果一样的话
                {
                    no.Content = item["content"].ToString();
                }
            }
            return no;
        }

        /// <summary>
        /// 此方法返回游戏公告
        /// </summary>
        /// <returns></returns>
        public async static Task<ObservableCollection<Notice>> GetTwo()
        {
            var list = new ObservableCollection<Notice>();
            string morestring = await MyHttpClient.GetJson(MoreUrl);      //完整公告
            JObject jo = JObject.Parse(await GetTips(1));
            JArray morestr = JArray.Parse(JObject.Parse(morestring)["data"]["list"].ToString());
            JArray jas = (JArray)jo["list"];
            foreach (JToken ja in jas)
            {
                list.Add(InitNotice(ja, ref morestr));
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

        public string Content { get; set; }
    }
}
