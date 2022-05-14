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
using GenshinImpact_Lanucher.Utils;
using System.IO;

namespace GenshinImpact_Lanucher.ViewModels
{
    public class SettingVM: ObservableRecipient
    {
        ProxyJson xml { get; set; }
        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public SettingVM()
        {
            IsActive = true;
            myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
            GameIni = new Launcher_Ini($@"{myini.IniReadValue("MyLanucherConfig", "GamePath")}/config.ini");
            StartArgs = myini.GetAgument();
            RadServerCheck = new RelayCommand(() => setradio());
            HeightGameSizeTextChanged = new RelayCommand<string >((txt) => heightchanged(txt));
            WidthGameSizeTextChanged = new RelayCommand<string>((txt) => widthchanged(txt));
            WindowCheck = new RelayCommand(() => windowcheck());


            _ServerPath = string.IsNullOrWhiteSpace(
                myini.IniReadValue("MyLanucherConfig", "Port"))?
                "": myini.IniReadValue("MyLanucherConfig", "Port");


            SelectServerPath = new RelayCommand(() => selectserverpath());
            //xml = new ProxyXml($@"{docpath}\GSIConfig\Config\Proxy.xml");
            var b = myini.IniReadValue("MyLanucherConfig", "ProxyPath");
            //if (!string.IsNullOrWhiteSpace(b))
            //{
            //    File.CreateText(xml.Path).Dispose();
            //    xml.CreateHeader();
            //    _ServerPath = xml.Path;
            //}
            SelectGamePath = new RelayCommand(()=>selectpath());
            WindowPop = new RelayCommand(() => popopen());
            if(StartArgs.GameServer == Server.B站)
            {
                Server2 = true;
            }
            else
            {
                Server1 = true;
            }
        }

        private void selectserverpath()
        {
            myini.IniWriteValue("MyLanucherConfig", "Port", _ServerPath);
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
        private  void setradio()
        {
            if(Server1 == true)
            {
                 GameIni.GameLauncherWrite( Launcher_Ini.Server.官服);
                myini.GameLauncherWrite(Server.官服);
            }
            else
            {
                GameIni.GameLauncherWrite(Launcher_Ini.Server.B站);
                myini.GameLauncherWrite(Server.B站);
            }

        }


        private bool server1;

        public bool Server1
        {
            get => server1;
            set=>SetProperty(ref server1, value);
        }


        private bool server2;

        public bool Server2
        {
            get => server2;
            set => SetProperty(ref server2, value);
        }


        private string ServerPath;

        public string _ServerPath
        {
            get { return ServerPath; }
            set { ServerPath = value; OnPropertyChanged(); }
        }       




        public RelayCommand RadServerCheck { get; set; }

        public RelayCommand WindowCheck { get; private set; }
        public RelayCommand WindowPop { get; private set; }
        public RelayCommand<string> HeightGameSizeTextChanged { get; set; }
        public RelayCommand SaveServer { get; set; }
        public RelayCommand SelectServerPath { get; set; }
        public RelayCommand<string> WidthGameSizeTextChanged { get; set; }
        public RelayCommand SelectGamePath { get; private set; }
        Launcher_Ini myini { get; set; }
        Launcher_Ini GameIni { get; set; }
        private StartAgument startargs;
        public StartAgument StartArgs
        {
            get 
            {
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
