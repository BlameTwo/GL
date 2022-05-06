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
            await startAgument.GO(myini.GetAgument());
        }

        Launcher_Ini myini { get; set; }
        public AsyncRelayCommand StartGame { get; private set; }
    }
}
