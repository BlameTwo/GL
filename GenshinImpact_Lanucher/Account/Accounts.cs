using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GenshinImpact_Lanucher.Account
{
    /// <summary>
    /// 暂停开发
    /// </summary>
    public class Accounts
    {
        private const string API_SALT = "xV8v4Qu54lUKrEYFZkJhB8cuOh9Asafs";
        private const string API_APP_VERSION = "2.11.1";
        private const string API_CLIENT_TYPE = "5";
        public Accounts()
        {

        }
        string Coolie = "mi18nLang=zh-cn; _MHYUUID=f8a8f84f-6d18-4488-9af1-c8cc2311f429; aliyungf_tc=8114bf4455d03339a69ca67da54129ac9e52a619097e624dc8602ce7b5c83e0a; acw_tc=781bad3c16523650193398572e68dbe055f365eadb8fa3a12247fed8951bb8; _ga=GA1.2.1331080635.1652366118; _gid=GA1.2.1917244687.1652366118; ltoken=bpRCe87c4XfcFnXYWZ9FgJFvDcHmf3bAhXMRxUde; ltuid=306606452; cookie_token=9YktQqc20NsHPlXKcVeyXkgbPwuFv1VI9qvr7Djo; account_id=306606452";
        public string GetAccount(string UID)
        {
            using(WebClient webclient = new WebClient())
            {
                webclient.Encoding = Encoding.UTF8;
                webclient.Headers["x-rpc-client_type"] = API_CLIENT_TYPE;
                webclient.Headers["x-rpc-app_version"] = API_APP_VERSION;
                webclient.Headers["Cookie"] = Coolie;
                string a = webclient.DownloadString("https://api-takumi.mihoyo.com/game_record/genshin/api/spiralAbyss?schedule_type=1&server=cn_qd01&role_id=500934368");
            }
            return "";
        }
    }
}
