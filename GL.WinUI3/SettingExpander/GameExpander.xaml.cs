using GL.WinUI3;
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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static GL.WinUI3.Model.Launcher_Ini;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyApp1.SettingExpander
{
    public sealed partial class GameExpander : Expander
    {
        public GameExpander()
        {
            this.InitializeComponent();
            Tip = new TeachingTip();
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
            GameIni = new Launcher_Ini($@"{myini.IniReadValue("MyLanucherConfig", "GamePath")}/config.ini");
            var StartArgs = myini.GetAgument();
            GameWidth.Text = string.IsNullOrWhiteSpace(myini.IniReadValue("MyLanucherConfig", "Width")) ? "" : myini.IniReadValue("MyLanucherConfig", "Width");
            GameHeight.Text = string.IsNullOrWhiteSpace(myini.IniReadValue("MyLanucherConfig", "Height")) ? "" : myini.IniReadValue("MyLanucherConfig", "Height");
            if (StartArgs.GameServer == Server.B站)
                Server2.IsChecked = true;
            else
                Server1.IsChecked = true;
            GameFull.IsOn =Convert.ToBoolean(string.IsNullOrWhiteSpace(myini.IniReadValue("MyLanucherConfig", "IsFull"))?"False": myini.IniReadValue("MyLanucherConfig", "IsFull"));
            GameBorder.IsOn =Convert.ToBoolean(string.IsNullOrWhiteSpace(myini.IniReadValue("MyLanucherConfig", "IsPop"))?"False": myini.IniReadValue("MyLanucherConfig", "IsPop"));
            GameFPS.IsOn =Convert.ToBoolean(string.IsNullOrWhiteSpace(myini.IniReadValue("MyLanucherConfig", "FPS"))?"False": myini.IniReadValue("MyLanucherConfig", "FPS"));
            GamePath.Text = string.IsNullOrWhiteSpace(myini.IniReadValue("MyLanucherConfig", "GamePath")) ? "" : myini.IniReadValue("MyLanucherConfig", "GamePath");
        }


        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Launcher_Ini myini { get; set; }
        Launcher_Ini GameIni { get; set; }

        private void Server_Check(object sender, RoutedEventArgs e)
        {
            RadioButton radio =  sender as RadioButton;
            switch (radio.Content.ToString())
            {
                case "官服":
                    if ((bool)radio.IsChecked)
                    {
                        GameIni.GameLauncherWrite(Launcher_Ini.Server.官服);
                        myini.GameLauncherWrite(Server.官服);
                        GL.WinUI3.Model.Resource.BilibiliSDK(false);
                        (App.MainWindow as MainWin).Tip.Title = "修改官服成功！";
                        (App.MainWindow as MainWin).Tip.Subtitle = "😊";
                        (App.MainWindow as MainWin).Tip.IsOpen = true;
                        return;
                    }
                    break;
                case "B服":
                    if ((bool)radio.IsChecked)
                    {
                        GameIni.GameLauncherWrite(Launcher_Ini.Server.B站);
                        myini.GameLauncherWrite(Server.B站);
                        GL.WinUI3.Model.Resource.BilibiliSDK(true);
                        (App.MainWindow as MainWin).Tip.Title = "修改B服成功！";
                        (App.MainWindow as MainWin).Tip.Subtitle = "😊";
                        (App.MainWindow as MainWin).Tip.IsOpen = true;
                        return;
                    }
                    break;
            }

            (App.MainWindow as MainWin).Tip.Title = "出现错误啦！";
            (App.MainWindow as MainWin).Tip.Subtitle = "😒";
            (App.MainWindow as MainWin).Tip.IsOpen = true;
        }



        public TeachingTip Tip
        {
            get { return (TeachingTip)GetValue(TipProperty); }
            set { SetValue(TipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Tip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipProperty =
            DependencyProperty.Register("Tip", typeof(TeachingTip), typeof(GameExpander), new PropertyMetadata(default(TeachingTip)));

        private void Width_Changed(object sender, TextChangedEventArgs e)
        {

            myini.IniWriteValue("MyLanucherConfig", "Width", (sender as TextBox).Text);
        }

        private void Height_Changed(object sender, TextChangedEventArgs e)
        {

            myini.IniWriteValue("MyLanucherConfig", "Height", (sender as TextBox).Text);
            
        }

        private void Full_Toggled(object sender, RoutedEventArgs e)
        {
            myini.IniWriteValue("MyLanucherConfig", "IsFull", (sender as ToggleSwitch).IsOn.ToString());
        }
        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            var window = new Microsoft.UI.Xaml.Window();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            var folderPicker = new Windows.Storage.Pickers.FileOpenPicker();
            folderPicker.FileTypeFilter.Add("*");
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);
            var folder = await folderPicker.PickSingleFileAsync();
            if (folder != null)
            {
                if (File.Exists(folder.Path + "YuanShen.exe")|| File.Exists(folder.Path + "GenshinImpact.exe"))
                {
                    myini.IniWriteValue("MyLanucherConfig", "GamePath", folder.Path);
                    (App.MainWindow as MainWin).Tip.Title = "设置成功！！";
                    (App.MainWindow as MainWin).Tip.Subtitle = "😊，找到可执行文件啦！";
                    (App.MainWindow as MainWin).Tip.IsOpen = true;
                }
            };
        }

        private void Pop_Toggle(object sender, RoutedEventArgs e)
        {

            myini.IniWriteValue("MyLanucherConfig", "IsPop", (sender as ToggleSwitch).IsOn.ToString());
        }

        private void Fps_Toggled(object sender, RoutedEventArgs e)
        {
            myini.IniWriteValue("MyLanucherConfig", "FPS", (sender as ToggleSwitch).IsOn.ToString());
        }
    }
}
