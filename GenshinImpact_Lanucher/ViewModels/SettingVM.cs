using GenshinImpact_Lanucher.Model;
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
using GenshinImpact_Lanucher.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using WPFUI.Appearance;
using System.Windows.Media.Imaging;
using Microsoft.VisualBasic.Devices;


namespace GenshinImpact_Lanucher.ViewModels
{

    public class SettingVM: ObservableRecipient
    {
        ProxyController proxy = new ProxyController();
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
            SaveImage = new RelayCommand(() =>
            {
                Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog()
                {
                    Title = "选择一张图片"
                };
                if(open.ShowDialog() == true)
                {
                    myini.IniWriteValue("Style", "BackImage", open.FileName);
                    var bitmap = new BitmapImage(new Uri(open.FileName));
                    bitmap.DecodePixelHeight = 1000;
                    bitmap.DecodePixelWidth = 1000;
                    var win = (System.Windows.Application.Current.MainWindow as MainWindow);
                    WPFUI.Appearance.Watcher.Watch(win, BackgroundType.Unknown, true, true);
                    win.BackImage.Source = bitmap;
                    ImagePath = open.FileName;
                }
            });
            WindowCheck = new RelayCommand(() => windowcheck());
            SaveCookie = new RelayCommand(() => savecookie());
            CookieText = myini.IniReadValue("MyLanucherConfig", "Cookie");
            _ServerPath = string.IsNullOrWhiteSpace(
                myini.IniReadValue("MyLanucherConfig", "Port"))?
                "": myini.IniReadValue("MyLanucherConfig", "Port");
            _MyColors = new RelayCommand<object>((str) =>
            {
                ChangedColor(str);
            });

            FPSCheck = new RelayCommand(() =>
            {
                Openfps();
            });

            ImagePath  = myini.IniReadValue("Style", "BackImage");
            BlurChanged = new RelayCommand<double>((number) =>
            {
                (System.Windows.Application.Current.MainWindow as MainWindow).WindowBlur.Radius = number; ;
                myini.IniWriteValue("Style", "Blur", number.ToString());
                
            });
            TranChanged = new RelayCommand<double>((number) =>
            {
                myini.IniWriteValue("Style", "Tran", number.ToString());
                (System.Windows.Application.Current.MainWindow as MainWindow).BackImage.Opacity = number;
            });
            SelectServerPath = new RelayCommand(() => selectserverpath());
            var b = myini.IniReadValue("MyLanucherConfig", "ProxyPath");
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
            switch (myini.IniReadValue("Style","Theme"))
            {
                case "Auto":
                    _ColorSelect = 0;
                    break;
                case "Light":
                    _ColorSelect = 1;
                    break;
                case "Dark":
                    _ColorSelect = 2;
                    break;
            }
            _Tran =   double.Parse(myini.IniReadValue("Style", "Tran"));
            _Blur =  double.Parse(myini.IniReadValue("Style", "Blur"));
            OpenPfx = new RelayCommand(() =>
            {
                ProxyController.proxyServer = new Titanium.Web.Proxy.ProxyServer();
                ProxyController.proxyServer.CertificateManager.EnsureRootCertificate();
            });

            ClosePfx = new RelayCommand(() =>
            {
                ProxyController.proxyServer = new Titanium.Web.Proxy.ProxyServer();
                ProxyController.UninstallCertificate();
            });

            RepairPfx = new RelayCommand(() =>
            {
                ProxyController.port = "11451";
                ProxyController.fakeHost = "127.0.0.1";
                ProxyController.Start();
                ProxyController.Stop();
                WindowTip.TipShow("修复完毕", "请尝试打开浏览器并验证，如果还是不行，请先卸载证书，随后再次修复！", WPFUI.Common.SymbolRegular.Earth16);
            });

            _IsMica = System.Convert.ToBoolean(myini.IniReadValue("Style", "IsMica"));

            MicaChanged = new RelayCommand(() =>
            {
                if (_IsMica)
                {
                    var version = new ComputerInfo().OSFullName;

                    if(version.IndexOf("11") != -1)   //为真的时候的条件
                    {
                        WindowTip.TipShow("Win11以下计算机不支持开启Mica"
                            , "当前只能使用图片背景哦！",
                            WPFUI.Common.SymbolRegular.Eraser20);
                        isMica = false;
                    }
                    else
                    {
                        MainWindow main = (System.Windows.Application.Current.MainWindow as MainWindow);
                        WPFUI.Appearance.Watcher.Watch(main, BackgroundType.Mica, true, true);
                        main.BackImage.Source = null;
                    }

                }
                myini.IniWriteValue("Style", "IsMica",_IsMica.ToString());
            });
        }

        private void Openfps()
        {
            myini.IniWriteValue("MyLanucherConfig", "FPS", _OpenFps.ToString());
        }

        private double Blur;

        public double _Blur
        {
            get => Blur;
            set => SetProperty(ref Blur,value);
        }


        private bool isMica;

        public bool _IsMica
        {
            get { return isMica; }
            set { isMica = value; }
        }



        private double Tran;

        public double _Tran
        {
            get => Tran;
            set => SetProperty(ref Tran, value);
        }

        private string ImagePath;

        public string _ImagePath
        {
            get => ImagePath;
            set => SetProperty(ref ImagePath, value);
        }


        private void ChangedColor(object str)
        {
            string swstr;
            if (str is ComboBoxItem)
                swstr = (str as ComboBoxItem).Content.ToString();
            else
                swstr = str.ToString();
            switch (swstr)
            {
                case "跟随系统":
                    MainWindow main = (System.Windows.Application.Current.MainWindow as MainWindow);
                    WPFUI.Appearance.Watcher.Watch(main, BackgroundType.Mica, true, true);
                    myini.IniWriteValue("Style", "Theme", "Auto");
                    _ColorSelect = 0;
                    break;
                case "浅色":
                    WPFUI.Appearance.Theme.Apply(
                    ThemeType.Light,
                    backgroundEffect: WPFUI.Appearance.BackgroundType.Mica,
                    true,
                    true);
                    myini.IniWriteValue("Style", "Theme", "Light");
                    break;
                case "深色":
                    WPFUI.Appearance.Theme.Apply(
                    ThemeType.Dark,
                    backgroundEffect: WPFUI.Appearance.BackgroundType.Mica,
                    true,
                    true);
                    myini.IniWriteValue("Style", "Theme", "Dark");
                    break;
            }
        }

        private int ColorSelect;

        public int _ColorSelect
        {
            get => ColorSelect;
            set => SetProperty(ref ColorSelect, value);
        }

        private void savecookie()
        {
            myini.IniWriteValue("MyLanucherConfig", "Cookie", _CookieText);
        }

        private void selectserverpath()
        {
            myini.IniWriteValue("MyLanucherConfig", "Port", _ServerPath);
        }

        private string CookieText;

        public string _CookieText
        {
            get => CookieText;
            set => SetProperty(ref CookieText, value);
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
                if (!(File.Exists(Path.Combine(openfolder.SelectedPath, "GenshinImpact.exe")) | File.Exists(Path.Combine(openfolder.SelectedPath, "YuanShen.exe"))))
                {
                    TipWindow.Show("找不到游戏文件", "注意：是原神游戏的运行目录而不是官方启动器的运行目录哦！", WPFUI.Common.SymbolRegular.ErrorCircle24);
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
                 GenshinImpact_Lanucher.Model.Resource.BilibiliSDK(false);
            }
            else
            {
                GameIni.GameLauncherWrite(Launcher_Ini.Server.B站);
                myini.GameLauncherWrite(Server.B站);
                GenshinImpact_Lanucher.Model.Resource.BilibiliSDK(true);
            }

        }


        private bool server1;

        public bool Server1
        {
            get => server1;
            set=>SetProperty(ref server1, value);
        }


        private bool OpenFps;

        public bool _OpenFps
        {
            get { return OpenFps; }
            set => SetProperty(ref OpenFps, value);
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
        public RelayCommand FPSCheck { get; private set; }
        public RelayCommand WindowPop { get; private set; }
        public RelayCommand<string> HeightGameSizeTextChanged { get; set; }
        public RelayCommand SaveServer { get; set; }
        public RelayCommand SaveImage { get; set; }
        public RelayCommand SelectServerPath { get; set; }
        public RelayCommand<double> BlurChanged { get; set; }
        public RelayCommand<double> TranChanged { get; set; }
        public RelayCommand<string> WidthGameSizeTextChanged { get; set; }
        public RelayCommand SelectGamePath { get; private set; }
        public RelayCommand SaveCookie { get; private set; }
        public RelayCommand OpenPfx { get; set; }
        public RelayCommand ClosePfx { get; set; }
        public RelayCommand RepairPfx { get; set; }
        public RelayCommand MicaChanged { get; set; }
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

        public RelayCommand<object> _MyColors { get; set; }

    }
}
