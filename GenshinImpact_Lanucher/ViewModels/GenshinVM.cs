﻿
using GenshinImpact_Lanuncher.Model;
using GenshinImpact_Lanuncher.Properties;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GenshinImpact_Lanuncher.ViewModels
{
    public class GenshinVM: ObservableRecipient
    {
        public GenshinVM()
        {
            IsActive = true;
            GoToSetting = new RelayCommand(() =>
            {
                
            });

            myini = new Launcher_Ini($@"{Resource.docpath}/GSIConfig/Config/LauncherConfig.ini");

            
        }





        public Launcher_Ini myini { get; set; }

        public RelayCommand GoToSetting { get; set; }


    }
}
