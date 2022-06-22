
using MyApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.WinUI3.Models
{
    public static class TipWindow
    {
        public static void Show(string title, string subtitle)
        {
            (App.MainWindow as MainWin).Tip.Title = title;
            (App.MainWindow as MainWin).Tip.Subtitle = subtitle;
            (App.MainWindow as MainWin).Tip.IsOpen = true;
        }
    }
}