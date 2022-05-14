using GenshinImpact_Lanucher.Model;
using GenshinImpact_Lanucher.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenshinImpact_Lanucher.ViewModels
{
    public class DefaultGameVM: ObservableRecipient
    {
        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Launcher_Ini myini { get; set; }
        public DefaultGameVM()
        {
            StartGame = new AsyncRelayCommand(async () =>await start());
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
            StartServerGame = new RelayCommand<bool>(async (bo) =>
            {
                if (myini.IniReadValue("Server", "IP") == null || myini.IniReadValue("Server", "IP").Equals(""))
                {
                    WindowTip.TipShow("代理设置错误", "旅行者，您似乎还没有配置代理呢", WPFUI.Common.SymbolRegular.ErrorCircle24);
                    return;
                };
                //这并不是打开游戏哦
                await startAgument.ServerGo(bo,myini.GetAgument());
                //string a =  MyHttpClient.GetJson($@"https://ys.mihoyo.com/status/server");
            });
        }
        StartGame startAgument = new StartGame();
        private async Task start()
        {
            StartGame startAgument = new StartGame();
            string a = await startAgument.GO(myini.GetAgument()) ;
            if (a == "1")
            {
                WindowTip.TipShow("游戏已经启动", "从外部启动游戏成功！如果出现闪退请检查游戏文件夹", WPFUI.Common.SymbolRegular.CheckmarkCircle24);
            }
            else
            {
                WindowTip.TipShow("游戏启动失败", "请检查游戏路径是否设置正确", WPFUI.Common.SymbolRegular.ErrorCircle24);
            };
        }

        public AsyncRelayCommand StartGame { get; private set; }

        public RelayCommand<bool> StartServerGame { get; private set; }
    }
}
