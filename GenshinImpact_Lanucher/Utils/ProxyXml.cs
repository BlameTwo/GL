using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        public ObservableCollection<ProxyArgs> ServerProfiles;



        public void SaveProfiles()
        {
            var json = JsonConvert.SerializeObject(ServerProfiles);
            Console.WriteLine(json);
            File.WriteAllText(Path, json);
        }


        public string Path;
        public ProxyXml(string XmlPath)
        {
            Path = XmlPath;
            ReadValue();
        }



        public void ReadValue()
        {
            if (!File.Exists(Path))
            {
                File.Create(Path).Close();
            }
            else
            {
                var profiles = File.ReadAllText(Path);
                try
                {
                    ServerProfiles = JsonConvert.DeserializeObject<ObservableCollection<ProxyArgs>>(profiles);

                }
                catch (Exception ex)
                {
                    ServerProfiles = new ObservableCollection<ProxyArgs>();
                }
            }

            if (ServerProfiles == null)
            {
                ServerProfiles = new ObservableCollection<ProxyArgs>();
            }

        }





        public async Task<bool> Delete(ProxyArgs args)
        {
            return ServerProfiles.Remove(args);

            SaveProfiles();
        }



        /// <summary>
        /// 插入服务器段
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<bool> Add(ProxyArgs args)
        {
            if (ServerProfiles == null)
            {
                ServerProfiles = new ObservableCollection<ProxyArgs>();
            }
            ServerProfiles.Add(args);
            SaveProfiles();
            return true;
        }
    }
}
