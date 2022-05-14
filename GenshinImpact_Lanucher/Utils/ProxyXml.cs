using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace GenshinImpact_Lanucher.Utils
{

    public class ProxyArgs
    {
        public string Host { get; set; }
        public string Name { get; set; }
    }

    public class ProxyXml
    {
        XmlDocument XmlDoc = new XmlDocument();
        public string Path;
        public ProxyXml(string XmlPath)
        {
            Path = XmlPath;
        }

        public async void CreateHeader()
        {
            await Task.Run(() =>
            {
                XmlDocument xmldoc = new XmlDocument();
                var node = xmldoc.CreateXmlDeclaration("1.0", "utf-8","");
                xmldoc.AppendChild(node);
                XmlElement father = xmldoc.CreateElement("Proxys");
                xmldoc.AppendChild(father);
                xmldoc.Save(Path);
            });
        }


        public async Task<ObservableCollection<ProxyArgs>> ReadValue()
        {
            return await Task.Run(() =>
            {
                ObservableCollection<ProxyArgs> retval = new ObservableCollection<ProxyArgs>();
                XmlDoc.Load(Path);
                var xmlel = XmlDoc.SelectSingleNode("Proxys");
                var nodes = xmlel.SelectNodes("Proxy");
                foreach (XmlNode item in nodes)
                {
                    var xmlel2 = item  as XmlElement;
                    ProxyArgs arg = new ProxyArgs()
                    {
                        Host = xmlel2.GetAttribute("Host"),
                        Name = xmlel2.GetAttribute("Name")
                    };
                    retval.Add(arg);
                }
                return retval;
            });
        } 


        public async Task<bool> UpDate(ProxyArgs args)
        {
            return await Task.Run(async ()  =>
            {
                XmlDoc.Load(Path);
                var xmlel = XmlDoc.SelectSingleNode("Proxys");
                if(await Delete(args) == true)
                {
                    await Add(args);
                    return true;
                }
                //没有找到节点
                return false;
            });
        }


        public async Task<bool> Delete(ProxyArgs args)
        {
            return await Task.Run(() =>
            {
                XmlDoc.Load(Path);
                var xmlel = XmlDoc.SelectSingleNode("Proxys");
                if (ProxyExists(args, xmlel as XmlElement))
                {
                    //可以修改
                    var son = xmlel.SelectSingleNode($@".//Proxy[@Name='{args.Name}']");
                    xmlel.RemoveChild(son);
                    XmlDoc.Save(Path);
                    return true;
                }
                //没有找到节点
                return false;
            });
        }


        public bool ProxyExists(ProxyArgs args,XmlElement xmlel)
        {
            foreach (var item in xmlel.SelectNodes("Proxy"))
            {
                if (((XmlElement)item).GetAttribute("Name") == args.Name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 插入服务器段
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async  Task<bool> Add(ProxyArgs args)
        {
            return await Task.Run(() =>
            {
                XmlDoc.Load(Path);
                var xmlel = XmlDoc.SelectSingleNode("Proxys");
                if(!ProxyExists(args,xmlel as XmlElement))
                {
                    XmlElement son = XmlDoc.CreateElement("Proxy");
                    son.SetAttribute("Host", args.Host);
                    son.SetAttribute("Name", args.Name);
                    xmlel.AppendChild(son);
                    XmlDoc.Save(Path);
                    return true;
                }
                return false;
            });
        }
    }
}
