using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.WinUI3.WindowHelper
{
    public static class CreateWindows
    {
        public static Window GetWindow(string title,object Content)
        {
            Window win = new Window() { Title = title, ExtendsContentIntoTitleBar = true };
            Grid grid = new Grid();
            GridLength lenth = GridLength.Auto;
            grid.RowDefinitions.Add(new RowDefinition() { Height = lenth});
            grid.RowDefinitions.Add(new RowDefinition());
            Grid border1 = new Grid() { Padding = new Thickness(10)};
            ContentControl content = new ContentControl() { Content = Content };
            TextBlock header = new TextBlock() { Text = title,Margin = new Thickness(0),VerticalAlignment = VerticalAlignment.Top};
            border1.Children.Add(header);
            grid.Children.Add(border1);
            grid.Children.Add(content);
            Grid.SetRow(border1,0);
            Grid.SetRow(content,1);
            win.Content = grid;
            win.SetTitleBar(border1);
            return win;
        }
    }
}
