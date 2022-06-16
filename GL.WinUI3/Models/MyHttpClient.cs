using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GL.WinUI3.Models
{
    public class MyHttpClient
    {
        public async  static Task<string> GetJson(string url)
        {
            return await Task.Run(() =>
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.KeepAlive = false;
                    request.Method = "GET";         //请求方式为Get
                    request.ContentType = "application/json; charset=UTF-8";        //编码为utf-8，格式为json
                    request.AutomaticDecompression = DecompressionMethods.GZip;         //压缩方式
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                    return retString;
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }
    }
}
