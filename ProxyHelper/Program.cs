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
                        ProxyController.fakeHost = command.ToLower().Substring(5);
                        ProxyController.port = "11451";
                        ProxyController.Start();
                        Console.WriteLine("连接成功");
                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo()
                        {
                            FileName = Path.Combine(@"D:\Genshin Impact\Genshin Impact Game", "YuanShen.exe"),
                            Verb = "runas",
                            WorkingDirectory = @"D:\Genshin Impact\Genshin Impact Game",
                            UseShellExecute = true,
                        };
                        p.Start();
                        break;
                }

                switch (command.ToLower().StartsWith("stop"))
                {
                    case true:
                        ProxyController.Stop();
                        Console.WriteLine("断开连接");
                        break;
                }
                continue;
            }
        }
    }
}