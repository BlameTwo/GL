
using GenshinImpact_Lanuncher.Model;
using GenshinImpact_Lanuncher.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinImpact_Lanuncher.ViewModels
{
    public class MainWinVM: ObservableRecipient
    {
        public MainWinVM()
        {
            Loaded = new RelayCommand(() => loaded());

        }
        public Launcher_Ini ini { get; set; }
        private void loaded()
        {
            Directory.CreateDirectory($@"{StaticResource.MyDoc}\Config\");
            ini = new Launcher_Ini($@"{StaticResource.MyDoc}\Config\\LauncherConfig.ini");
            if (!File.Exists($@"{StaticResource.MyDoc}\Config\LauncherConfig.ini"))
            {
                File.CreateText($@"{StaticResource.MyDoc}\Config\LauncherConfig.ini").Dispose();
                ini.SaveDefaultSettingArgs();
            }        //这是判断是否存在配置文件
        }

        public RelayCommand Loaded { get; private set; }
    }
}
