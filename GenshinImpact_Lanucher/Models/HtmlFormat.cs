using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinImpact_Lanucher.Models
{
    public class HtmlFormat
    {
        /// <summary>
        /// 生成HTML
        /// </summary>
        /// <param name="body">HTML文本内容</param>
        /// <returns></returns>
        public string Format(string body)
        {
            string a = "<!DOCTYPE html><html><head><meta charset='UTF-8'><meta http-equiv='X-UA-Compatible' content='IE=edge'><meta name='viewport'content='width=device-width, initial-scale=1.0'><title>Document</title></head><body>";
            string b = "</body></html>";
            string value =  a + body + b;
            return value.Replace("&lt;t class=t_gl&gt;", "").Replace("&lt;/t&gt;", "");     //过滤T标签
        }
    }
}
