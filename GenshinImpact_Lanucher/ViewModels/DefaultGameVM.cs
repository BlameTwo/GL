using GenshinImpact_Lanucher.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinImpact_Lanucher.ViewModels
{
    public class DefaultGameVM: ObservableRecipient
    {
        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public DefaultGameVM()
        {
            StartGame = new AsyncRelayCommand(async () =>await start());
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
        }

        private async Task start()
        {
            StartGame startAgument = new StartGame();
            string a = await startAgument.GO(myini.GetAgument()) ;
            if (a == "1")
            {
                (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Message = "从外部启动游戏成功！如果出现闪退请检查游戏文件夹";
                (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Icon = WPFUI.Common.SymbolRegular.ErrorCircle24;
                (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Title = "游戏已经启动";       //返回的错误列表
                (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Show();
            };
        }

        Launcher_Ini myini { get; set; }
        public AsyncRelayCommand StartGame { get; private set; }
    }
}
