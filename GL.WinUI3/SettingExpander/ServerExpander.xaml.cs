using GL.WinUI3.Model;
using GL.WinUI3.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyApp1.SettingExpander
{
    public sealed partial class ServerExpander : Expander
    {
        public ServerExpander()
        {
            this.InitializeComponent();
            
        }

        bool Exits()
        {
            if(File.Exists(Resource.docpath+ @"\GSIConfig\Proxy\ProxyHelper.exe"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async  void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new Microsoft.UI.Xaml.Window();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            var folderPicker = new Windows.Storage.Pickers.FileOpenPicker();
            folderPicker.CommitButtonText = "选择服务器包";
            folderPicker.FileTypeFilter.Add(".zip");
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);
            var folder = await folderPicker.PickSingleFileAsync();
            if(folder != null)
            {
                System.IO.Compression.ZipFile.ExtractToDirectory(folder.Path,Resource.docpath+ @"\GSIConfig\Proxy");
                if(File.Exists(Resource.docpath + @"\GSIConfig\Proxy\ProxyHelper.exe"))
                {
                    (sender as Button).IsEnabled = false;
                    this.Exits();
                    TipWindow.Show("服务器补丁安装完毕！", "从现在开始可以使用服务器连接到他人的游戏中一同游玩。");
                }
                else
                {
                    TipWindow.Show("服务器补丁安装错误，可能是安装包选择错误", "请向Github提交iss反馈BUG！");
                    Directory.Delete(Resource.docpath + @"\GSIConfig\Proxy\");
                }
            }
        }

        private void LeftText_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
