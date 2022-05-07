using GenshinImpact_Lanucher.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static GenshinImpact_Lanucher.Model.Launcher_Ini;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows;
namespace GenshinImpact_Lanucher.ViewModels
{
    public class SettingVM: ObservableRecipient
    {

        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public SettingVM()
        {
            IsActive = true;
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
            GameIni = new Launcher_Ini($@"{LanucherRegistryKey.GetGamePath()}/config.ini");
            StartArgs = myini.GetAgument();
            RadServerCheck = new RelayCommand<System.Windows.Controls.RadioButton>((rad) => setradio(rad));
            HeightGameSizeTextChanged = new RelayCommand<string >((txt) => heightchanged(txt));
            WidthGameSizeTextChanged = new RelayCommand<string>((txt) => widthchanged(txt));
            WindowCheck = new RelayCommand(() => windowcheck());
            SelectGamePath = new RelayCommand(()=>selectpath());
            WindowPop = new RelayCommand(() => popopen());
        }


        private void popopen()
        {
            myini.IniWriteValue("MyLanucherConfig", "IsPop", StartArgs.IsPop.ToString());
        }

        private void selectpath()
        {
            FolderBrowserDialog openfolder = new FolderBrowserDialog();
            openfolder.Description = "选择游戏文件夹";
            if(openfolder.ShowDialog() == DialogResult.OK)
            {
                if (!System.IO.File.Exists($@"{openfolder.SelectedPath}\YuanShen.exe"))
                {
                    (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Message="注意：是原神游戏的运行目录而不是官方启动器的运行目录哦！";
                    (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Icon =  WPFUI.Common.SymbolRegular.ErrorCircle24;
                    (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Title = "找不到游戏文件";
                    (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Show();
                }
                else
                {
                    myini.IniWriteValue("MyLanucherConfig","GamePath",openfolder.SelectedPath);
                }
                StartArgs = myini.GetAgument();
            }
        }

        private void windowcheck()
        {
            myini.IniWriteValue("MyLanucherConfig", "IsFull", StartArgs.full.ToString()) ;
        }

        private void heightchanged(string  txt)
        {
            myini.IniWriteValue("MyLanucherConfig", "Height",txt);
            StartArgs.GameHeight = txt;
        }


        private void widthchanged(string txt)
        {
            myini.IniWriteValue("MyLanucherConfig", "Width", txt);
            StartArgs.GameWidth = txt;
        }
        private async  void setradio(System.Windows.Controls.RadioButton rad)
        {
            if(Server1 == true)
            {
                await GameIni.GameLauncherWrite( Launcher_Ini.Server.官服);
            }
            else if(Server2 == true)
            {
                await GameIni.GameLauncherWrite( Launcher_Ini.Server.B站);
            }
                
        }

        private bool server1;

        public bool Server1
        {
            get { return server1; }
            set { server1 = value; OnPropertyChanged(); }
        }


        private bool server2;

        public bool Server2
        {
            get { return server2; }
            set { server2 = value;OnPropertyChanged(); }
        }

        public RelayCommand<System.Windows.Controls.RadioButton> RadServerCheck { get; private set; }
        public RelayCommand WindowCheck { get; private set; }
        public RelayCommand WindowPop { get; private set; }
        public RelayCommand<System.Windows.Controls.RadioButton> RadServer2Check { get; private set; }
        public RelayCommand<string> HeightGameSizeTextChanged { get; set; }

        public RelayCommand<string> WidthGameSizeTextChanged { get; set; }
        public RelayCommand SelectGamePath { get; private set; }
        Launcher_Ini myini { get; set; }
        Launcher_Ini GameIni { get; set; }
        private StartAgument startargs;
        public StartAgument StartArgs
        {
            get 
            {
                if (startargs.GameServer == Launcher_Ini.Server.官服)
                    Server1 = true;
                else
                    Server2 = true;
                return startargs;
            }
            set 
            {
                startargs = value;
                OnPropertyChanged();
            }
        }
    }
}
