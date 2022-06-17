using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace GenshinImpact_Lanuncher.Utils
{

    public class ProxyArgs
    {
        public string Host { get; set; }
        public string Name { get; set; }
    }



    public class ProxyJson
    {

        public ObservableCollection<ProxyArgs> ServerProfiles;



        public bool SaveProfiles()
        {
            var json = JsonConvert.SerializeObject(ServerProfiles);
            Console.WriteLine(json);
            File.WriteAllText(Path, json);
            return true;
        }


        public string Path;
        public ProxyJson(string XmlPath)
        {
            Path = XmlPath;
            ReadValue(XmlPath);
        }



        public void ReadValue(string path)
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

            if (ServerProfiles == null)
            {
                ServerProfiles = new ObservableCollection<ProxyArgs>();
            }
        }





        public async Task<bool> Delete(ProxyArgs args)
        {
            return await Task.Run(() =>
            {
                foreach (var item in ServerProfiles)
                {
                    if(item.Name == args.Name)
                    {
                        var r = ServerProfiles.Remove(item);
                        SaveProfiles();
                        return r;
                    }
                }
                SaveProfiles();
                return false;
            });
        }



        /// <summary>
        /// 插入服务器段
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<bool> Add(ProxyArgs args)
        {
            return await Task.Run(() =>
            {
                if (ServerProfiles == null)
                {
                    ServerProfiles = new ObservableCollection<ProxyArgs>();
                }
                ServerProfiles.Add(args);
                SaveProfiles();
                return true;
            });
        }
    }
}
