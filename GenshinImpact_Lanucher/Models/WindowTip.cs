using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinImpact_Lanuncher.Models
{
    public static class WindowTip
    {
        public static void TipShow(string title,string subtitle, WPFUI.Common.SymbolRegular icon)
        {
            (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Message = subtitle;
            (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Icon = icon;
            (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Title = title;       //返回的错误列表
            (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Show();
        } 
    }
}
