using GL.WinUI3;
using GL.WinUI3.EventArgs;
using GL.WinUI3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using MyApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp1.ViewModel
{
    public class NewStartConfigViewModel:ObservableRecipient, IRecipient<ExeArgs>
    {
        public NewStartConfigViewModel()
        {
            IsActive = true;
            _ConfigJson = new StartConfigJson()
            {
                Name="B服启动配置文件",
                Config = new GL.WinUI3.Model.StartAgument()
                {
                    full = true,
                    IsFPS = true,
                    IsPop = true,
                    GameWidth = "1980",
                    GameHeight = "1080",
                    GamePath = @"D:\Genshin Impact\Genshin Impact Game",
                    GameServer = "官服"
                },
                ExeList = new ObservableCollection<ExeConfig>()
            };

            OpenFolder = new RelayCommand(async () =>
            {
                var window = new Microsoft.UI.Xaml.Window();
                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
                var folderPicker = new Windows.Storage.Pickers.FolderPicker();
                folderPicker.FileTypeFilter.Add("*");
                WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);

                var folder = await folderPicker.PickSingleFolderAsync();
                if (folder != null)
                {
                    if (File.Exists(folder.Path + @"\YuanShen.exe") || File.Exists(folder.Path + @"\GenshinImpact.exe"))
                    {
                        _ConfigJson.Config.GamePath = folder.Path;
                        TipWindow.Show("此路径没有任何问题", "😊，找到可执行文件啦！");
                    }
                };
            });

            SaveConfig = new RelayCommand(async () =>
            {
                _ConfigJson.Config.GameServer = _MyServer1 == true ? "官服" : "B服";
                StartConfigReadAndWrite save = new StartConfigReadAndWrite();
                if (await save.SaveAdd(_ConfigJson))
                {
                    TipWindow.Show("新建启动文件成功", "现可回到启动页面使用配置文件启动");
                }
            });
        }

        public void Receive(ExeArgs message)
        {
            if (Exit(message.Config) == false)
            {
                if(message.ExeEnum == ExeEnum.Add)
                    _ConfigJson.ExeList.Add(message.Config);
            }
            if(message.ExeEnum == ExeEnum.Remove)
            {
                _ConfigJson.ExeList.Remove(message.Config);
            }
            
        }

        bool Exit(ExeConfig exeConfig)
        {
            foreach (var item in _ConfigJson.ExeList)
            {
                if(exeConfig.Path == item.Path)
                {
                    return true;
                }
            }
            return false;
        }

        private bool Server1;

        public bool _MyServer1
        {
            get { return Server1; }
            set => SetProperty(ref Server1, value);
        }

        private bool Server2;

        public bool _MyServer2
        {
            get { return Server2; }
            set => SetProperty(ref Server2, value);
        }


        private StartConfigJson ConfigJson;

        public StartConfigJson _ConfigJson
        {
            get
            {
                return ConfigJson;
            }
            set 
            {
                if(value.Config.GameServer == "官服")
                {
                    _MyServer1 = true;
                }
                else
                {
                    _MyServer1 = true;
                }
                SetProperty(ref ConfigJson, value);
            }
        }

        public RelayCommand OpenFolder { get; private set; } 
        public RelayCommand SaveConfig { get; private set; }


    }
}
