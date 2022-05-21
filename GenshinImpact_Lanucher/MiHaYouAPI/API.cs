using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        /// 获得米哈游账号信息，需要Cookie
        /// </summary>
        /// <returns></returns>
        static Task<JObject> GetMhy_Account_JObject()
        {
            return Task.Run(() =>
            {
                return Request(x => x.DownloadString("https://bbs-api.mihoyo.com/user/wapi/getUserFullInfo?gids=2"),
                    CreateDynamicSecret("https://bbs-api.mihoyo.com/user/wapi/getUserFullInfo?gids=2", ""));
            });
        }


        public static Task<MiHaYouAccountArgs> GetMiHaYouAccount()
        {
            return Task.Run(async () =>
            {
                JObject jo = await GetMhy_Account_JObject();
                MiHaYouAccountArgs args = new MiHaYouAccountArgs()
                {
                     Uid = jo["data"]["user_info"]["uid"].ToString(),
                     AccountName = jo["data"]["user_info"]["nickname"].ToString(),
                     AccountImage = jo["data"]["user_info"]["avatar_url"].ToString()
                };
                return args;
            });
        }


    }
}
