using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GenshinImpact_Lanucher.MiHaYouAPI
{

    public static class MiHaYouArgs
    {
        public static string Cookie { get; set; }
    }

    //原神树脂API

    public static class API
    {
        private const string API_SALT = "xV8v4Qu54lUKrEYFZkJhB8cuOh9Asafs";
        private const string API_APP_VERSION = "2.11.1";
        private const string API_CLIENT_TYPE = "5";


        private static JObject Request(Func<WebClient, string> func, string ds)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers["x-rpc-client_type"] = API_CLIENT_TYPE;
                client.Headers["x-rpc-app_version"] = API_APP_VERSION;
                client.Headers["DS"] = ds;
                client.Headers["Cookie"] = MiHaYouArgs.Cookie;
                string response = func(client);
                return JObject.Parse(response);
            }

        }

        private static string CreateDynamicSecret(string url, string body)
        {
            string query = "";
            string[] urlPart = url.Split('?');
            if (urlPart.Length == 2)
            {
                string[] parameters = urlPart[1].Split('&').OrderBy(x => x).ToArray();
                query = string.Join("&", parameters);
            }

            long time = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            string random = CreateRandomString(6);
            // Credits: lulu666lulu, https://github.com/Azure99/GenshinPlayerQuery/issues/20
            string check = ComputeMd5($"salt={API_SALT}&t={time}&r={random}&b={body}&q={query}");

            return $"{time},{random},{check}";
        }

        private static string CreateRandomString(int length)
        {
            StringBuilder builder = new StringBuilder(length);

            const string randomStringTemplate = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int pos = random.Next(0, randomStringTemplate.Length);
                builder.Append(randomStringTemplate[pos]);
            }

            return builder.ToString();
        }

        private static string ComputeMd5(string content)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(content ?? ""));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in result)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }


        /// <summary>
        /// Get方法
        /// </summary>
        /// <returns></returns>
        static Task<JObject> Get(string url,string body)
        {
            return Task.Run(() =>
            {
                return Request(x => x.DownloadString(url),
                    CreateDynamicSecret(url, body));
            });
        }



        /// <summary>
        /// MHY账号数据
        /// </summary>
        /// <returns></returns>
        public static Task<MiHaYouAccountArgs> GetMiHaYouAccount()
        {
            return Task.Run(async () =>
            {
                JObject jo = await Get(
                    "https://bbs-api.mihoyo.com/user/wapi/getUserFullInfo?gids=2"
                    ,"");
                MiHaYouAccountArgs args = new MiHaYouAccountArgs()
                {
                     Uid = jo["data"]["user_info"]["uid"].ToString(),
                     AccountName = jo["data"]["user_info"]["nickname"].ToString(),
                     AccountImage = jo["data"]["user_info"]["avatar_url"].ToString()
                };
                return args;
                //game_id=2得项目为原神，此后增加原神经验值查询
            });
        }

        /// <summary>
        /// 获得用户账号等级
        /// </summary>
        /// <returns></returns>
        public static Task<ObservableCollection< GenshinAccountArgs>> GetGenshinAccount()
        {
            return Task.Run(async () =>
            {
                JObject jo = await Get(
                    "https://api-takumi.mihoyo.com/binding/api/getUserGameRolesByCookie?game_biz=hk4e_cn", "");
                ObservableCollection <GenshinAccountArgs> args = new ObservableCollection<GenshinAccountArgs>();
                JArray ja = JArray.Parse(jo["data"]["list"].ToString());
                foreach (var item in ja)
                {
                    args.Add(new GenshinAccountArgs()
                    {
                         Level = item["level"].ToString(),
                         Name = item["nickname"].ToString(),
                         OwnerServer = item["region"].ToString(),
                         ServerName = item["region_name"].ToString(),
                         Uid = item["game_uid"].ToString()
                    });
                }
                return args;
            });
        }

        /// <summary>
        /// 获得每日详情
        /// </summary>
        /// <param name="server"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static Task<GenshinDayArgs> GenDay(string server,string uid)
        {
            return Task.Run(async () =>
            {
                var args = new GenshinDayArgs();
                string url = $"https://api-takumi-record.mihoyo.com/game_record/app/genshin/api/dailyNote?server={server}&role_id={uid}";
                JObject jo = await Get(url, "");
                //Data Not exists
                args.Current_resion = jo["data"]["current_resin"].ToString();
                args.max_resion = jo["data"]["max_resin"].ToString();
                args.Days = new ObservableCollection<DayTask>();
                foreach (var item in JArray.Parse(jo["data"]["expeditions"].ToString()))
                {
                    args.Days.Add(new DayTask() { IconPath = item["avatar_side_icon"].ToString(), status = item["status"].ToString(), end_Time = item["remained_time"].ToString() });
                }
                args.home_money = jo["data"]["current_home_coin"].ToString();
                args.max_home_money = jo["data"]["max_home_coin"].ToString();
                args.max_boss = jo["data"]["resin_discount_num_limit"].ToString();
                args.boss = jo["data"]["remain_resin_discount_num"].ToString();
                args.truetransoformer = System.Convert.ToBoolean(jo["data"]["transformer"]["obtained"].ToString());
                args.transformer =System.Convert.ToBoolean( jo["data"]["transformer"]["recovery_time"]["reached"].ToString());
                int day = int.Parse(jo["data"]["transformer"]["recovery_time"]["Day"].ToString());
                int hour = int.Parse(jo["data"]["transformer"]["recovery_time"]["Hour"].ToString());
                int minute = int.Parse(jo["data"]["transformer"]["recovery_time"]["Minute"].ToString());
                int second = int.Parse(jo["data"]["transformer"]["recovery_time"]["Second"].ToString());
                //做一个转换，只要整数部分
                args.transformertime =long.Parse( ((new TimeSpan(day,hour,minute,second).Ticks)/ 864000000000).ToString());
                return args;
            });
        }
    }
}
