using GL.WinUI3;
using GL.WinUI3.Model;
using GL.WinUI3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp1.ViewModel
{
    public class DefaultGameViewModel:ObservableRecipient
    {

        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Launcher_Ini myini { get; set; }
        public DefaultGameViewModel()
        {
            IsActive = true;
            StartGame = new AsyncRelayCommand(async () => await start());
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
        }

        private async Task start()
        {
            StartGame startAgument = new StartGame();
            string a = await startAgument.GO(myini.GetAgument());
            if (a == "1")
            {
                TipWindow.Show("启动游戏成功！", "可以快乐的玩耍了");
            }
            else
            {

                TipWindow.Show("启动失败，请检查游戏文件夹！", "😒");
            };
        }

        public AsyncRelayCommand StartGame { get; private set; }
    }
}
