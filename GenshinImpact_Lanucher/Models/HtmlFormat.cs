using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml;

namespace GenshinImpact_Lanuncher.Models
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
            return value.Replace("&lt;t class=\"t_lc\"&gt;\", ","").Replace("&lt;/t&gt;", "").Replace("&lt;t class=\"t_gl\"&gt;","");     //过滤T标签
        }

        public static Section FrrmatToFloutView(string HTML)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(HTML.Replace("&nbsp;", ""));
            Section section = new Section();
            //HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//p");
            //foreach (HtmlNode item in nodes)
            //{
            //    Paragraph p = new Paragraph();
            //    string a = item.InnerText;
            //    p.Inlines.Add(a);
            //    section.Blocks.Add(p);
            //}
            var value = doc.DocumentNode.SelectSingleNode("//body");
            foreach (var item in GetMore(value.OuterHtml))
            {
                section.Blocks.Add(item);
            }
            return section;
        }

        public static List<Paragraph> GetMore(string Html)
        {
            List<Paragraph> list = new List<Paragraph>();
            HtmlDocument htmldoc = new HtmlDocument();
            htmldoc.LoadHtml(Html);
            HtmlNode node = htmldoc.DocumentNode.SelectSingleNode("//body");
            HtmlNodeCollection nodes = node.ChildNodes;
            
            foreach (HtmlNode item in nodes)
            {
                switch (item.Name)
                {
                    case "ol":
                        {
                            foreach (var item2 in item.SelectNodes(".//li"))
                            {
                                Paragraph paragraph = new Paragraph();
                                foreach (var item3 in item2.SelectNodes(".//p"))
                                {
                                    paragraph.Inlines.Add(item.InnerText);
                                    
                                }
                                list.Add(paragraph);
                            }
                            break;
                        }
                    case "p":
                        {
                            Paragraph paragraph = new Paragraph();
                            //拿取标签
                            paragraph.Inlines.Add(item.InnerText);
                            list.Add(paragraph);
                            break;
                        }
                    case "table":
                        {
                            break;
                        }
                }
            }
            return list;
        }
    }
}
