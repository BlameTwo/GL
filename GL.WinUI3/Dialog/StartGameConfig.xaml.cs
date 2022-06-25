using GL.WinUI3;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyApp1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyApp1.Dialog
{
    public sealed partial class StartGameConfig : ContentDialog
    {
        public StartGameConfig()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (ExeName.Text == "" || ExePath.Text == "")
            {
                Tip.Text = "参数缺少";
                args.Cancel = true;
            }
            else
            {
                ExeConfig arg = new ExeConfig()
                {
                    Name = ExeName.Text,
                    Path = ExePath.Text,
                    Args = ExeArgs.Text
                };
                WeakReferenceMessenger.Default.Send(arg);
            }
        }

        private async  void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new Microsoft.UI.Xaml.Window();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            var folderPicker = new Windows.Storage.Pickers.FileOpenPicker();
            folderPicker.CommitButtonText = "选择文件";
            folderPicker.FileTypeFilter.Add(".exe");
            folderPicker.FileTypeFilter.Add(".lnk");
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);
            var folder = await folderPicker.PickSingleFileAsync();
            if(folder != null)
            {
                ExeName.Text = System.IO.Path.GetFileName(folder.Path);
                ExePath.Text = folder.Path;
            }
        }
    }
}
