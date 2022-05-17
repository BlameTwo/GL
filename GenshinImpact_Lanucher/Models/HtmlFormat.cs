using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml;

namespace GenshinImpact_Lanucher.Models
{
    public static class HtmlFormat
    {
        /// <summary>
        /// 生成HTML
        /// </summary>
        /// <param name="body">HTML文本内容</param>
        /// <returns></returns>
        public static string Format(string body)
        {
            string a = "<!DOCTYPE html><html lang='en'><head><meta charset='UTF-8'><meta http-equiv='X-UA-Compatible' content='IE=edge'><meta name='viewport' content='width=device-width, initial-scale=1.0'><title>Document</title></head><body>";
            string b = "</body></html>";
            string value =  a + body + b;
            return value.Replace("&lt;t class=t_gl&gt;", "").Replace("&lt;/t&gt;", "");     //过滤T标签
        }

        public static Section FrrmatToFloutView(string HTML)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(HTML.Replace("&nbsp;", ""));
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//p");
            Section section = new Section();
            foreach (HtmlNode item in nodes)
            {
                Paragraph p = new Paragraph();
                string a = item.InnerText;
                p.Inlines.Add(a);
                section.Blocks.Add(p);
            }
            return section;
        }
    }
}
