using GL.WinUI3.Model;
using MyApp1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.WinUI3.Models
{
    public class StartConfigJson
    {
        public string Name { get; set; }
        public StartAgument Config { get; set; }
        public ObservableCollection<ExeConfig> ExeList { get; set; } = new ObservableCollection<ExeConfig>();
    }


    public class StartConfigReadAndWrite
    {
        public async  Task<bool> SaveAdd(StartConfigJson json)
        {
            return await Task.Run(() =>
            {
                //下面加一个Config用来区分json文件
                string path = $@"{Resource.docpath}\GSIConfig\Config\{json.Name}Config.json";
                if (File.Exists(path))
                {
                    return false;
                }
                var json2 = JsonConvert.SerializeObject(json);
                File.WriteAllText(path,json2);
                return true;
            });
        }

        public Task<ObservableCollection<StartConfigJson>> Reads()
        {
            return Task.Run(() =>
            {
                var list = new ObservableCollection<StartConfigJson>();
                DirectoryInfo info = new DirectoryInfo($@"{Resource.docpath}\GSIConfig\Config\");
                var files = info.GetFiles("*.json");
                foreach (var item in info.GetFiles("*.json"))
                {
                    try
                    {
                        if (item.Name.IndexOf("Config") != -1)
                        {
                            string a = File.ReadAllText(item.FullName);
                            var model = JsonConvert.DeserializeObject<StartConfigJson>(a);
                            list.Add(model);
                        }
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("转换错误！");
                    }
                }
                return list;
            });
        }
    }

}
