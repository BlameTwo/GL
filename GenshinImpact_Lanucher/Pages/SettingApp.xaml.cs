﻿using GenshinImpact_Lanuncher.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GenshinImpact_Lanuncher.Pages
{
    /// <summary>
    /// SettingApp.xaml 的交互逻辑
    /// </summary>
    public partial class SettingApp : Page
    {
        public SettingApp()
        {
            InitializeComponent();
            this.DataContext = new SettingVM();
        }
    }
}
