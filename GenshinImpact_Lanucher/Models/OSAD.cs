using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenshinImpact_Lanuncher.Models
{
    public class OSAD
    {
        public OSAD()
        {

        }


        public async Task<OSADArgs> GetOSAD()
        {
            string retString = await MyHttpClient.GetJson("https://api.xygeng.cn/one");
            var joject = JObject.Parse(retString);
            OSADArgs args = new OSADArgs()
            {
                content = joject["data"]["content"].ToString(),
                tag=joject["data"]["tag"].ToString(),
                origin=joject["data"]["origin"].ToString()//.Replace("\r"," ")//去掉换行符。
            };
             return args;

        }

        public async Task<OSADArgs> GetOSADAsync()
        {
            var result = await Task.Run(() => GetOSAD());
            return result;
        }

       public  class OSADArgs
        {
            /// <summary>
            /// 内容
            /// </summary>
            public string content { get; set; }
            /// <summary>
            /// 作品
            /// </summary>
            public string tag { get; set; }

            /// <summary>
            /// 作者
            /// </summary>
            public string origin { get; set; }

        }
    }
}
