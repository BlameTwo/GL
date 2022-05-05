using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;


namespace GenshinImpact_Lanucher.Models
{
    public class Account
    {
        private static string GetStringAccountConfig(string key)
        {
            object value = Registry.GetValue(@"HKEY_CURRENT_USER\Software\miHoYo\原神", key, "");
            return Encoding.UTF8.GetString((byte[])value);
        }
        private static void SetStringAccountConfig(string key, string value)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\miHoYo\原神", key, Encoding.UTF8.GetBytes(value));
        }
        
        /// <summary>
        /// 获取当前帐号
        /// </summary>
        /// <returns></returns>
        public static string ReadUser()
        {
            return GetStringAccountConfig("MIHOYOSDK_ADL_PROD_CN_h3123967166");
        }

        public static string ReadConfig()
        {
            return GetStringAccountConfig("GENERAL_DATA_h2389025596");
        }


        public  Account ReadGameAccount(bool IsConfig)
        {
            Account acc = new Account();
            acc.Name = this.Name;
            acc.AccountUser = ReadUser();
            acc.AccountConfig = IsConfig ? ReadConfig():"Null";
            return acc;
        }


        

        public void SaveToFile(bool IsConfig)
        {
            Directory.CreateDirectory($@"{StaticResource.MyDoc}\User\");
            ReadGameAccount(true);
            File.WriteAllText($@"{StaticResource.MyDoc}\User\{Name}.json", new JavaScriptSerializer().Serialize(ReadGameAccount(IsConfig)));
        }


        public void DeleteFile(string name)
        {
            File.Delete($@"{StaticResource.MyDoc}\User\{name}.json");
        }

        public void WriteToRegedit()
        {
            if (string.IsNullOrWhiteSpace(AccountUser))
            {
                return;
            }
            else
            {
                SetStringAccountConfig("MIHOYOSDK_ADL_PROD_CN_h3123967166", AccountUser);
                if (!string.IsNullOrWhiteSpace(AccountConfig))
                {
                    SetStringAccountConfig("GENERAL_DATA_h2389025596", AccountConfig);
                }
            }
        }






        /// <summary>
        /// 账号加密信息
        /// </summary>
        public string AccountUser { get; set; }
        /// <summary>
        /// 账号的相关配置，一般无视
        /// </summary>
        public string AccountConfig { get; set; }

        /// <summary>
        /// 账号名称，需要外部指定
        /// </summary>
        public string Name { get; set; }

    }
}
