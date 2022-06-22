using GL.WinUI3.Model;
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
            if(File.Exists(Resource.docpath+ @"\GSIConfig\ProxyProxyHelper.exe"))
            {
                return true;
            }
            else
            {
                return false;
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
            }
        }
    }
}
