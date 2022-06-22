using System;
using System.Diagnostics;

namespace ProxyHelper // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string command = Console.ReadLine()!;
                if(command.ToLower() == "exit")
                {
                    Console.WriteLine("退出！");
                    break;
                }
                switch (command.ToLower().StartsWith("start"))
                {
                    case true:
                        string str = command.ToLower().Substring(5);
                        ProxyController.fakeHost = str;
                        ProxyController.port = "11451";
                        ProxyController.Start();
                        Console.WriteLine("连接成功");
                        break;
                }

                switch (command.ToLower().StartsWith("stop"))
                {
                    case true:
                        if (ProxyController._IsRun)
                        {
                            ProxyController.Stop();
                        }
                        Console.WriteLine("断开连接");
                        break;
                }


                switch (command.ToLower().StartsWith("setup"))
                {
                    case true:
                        ProxyController.SetupCertficate();
                        break;
                }
                switch (command.ToLower().StartsWith("unsetup"))
                {
                    case true:
                        ProxyController.UninstallCertificate();
                        break;
                }
                continue;
            }
        }
    }
}