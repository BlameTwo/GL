﻿using GenshinImpact_Lanucher.Model;
using GenshinImpact_Lanucher.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GenshinImpact_Lanucher
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                StartGame.Controller.Stop();
            }
            catch (Exception){}
            base.OnExit(e);
        }
    }
}
