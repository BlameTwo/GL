using GL.WinUI3;
using GL.WinUI3.MiHaYouAPI;
using GL.WinUI3.Model;
using GL.WinUI3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MyApp1.Models;
using MyApp1.View;
using MyApp1.View.Pages;
using MyApp1.WindowHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyApp1.ViewModel
{

    
    public class DefaultGameViewModel:ObservableRecipient
    {
        

        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public DefaultGameViewModel()
        {
            IsActive = true;
            StartGame = new RelayCommand(async () => await start());
            Resource.myini = new Launcher_Ini($@"{docpath}/GSIConfig/Config/LauncherConfig.ini");
            Loaded = new RelayCommand(async () =>
            {
                StartConfigReadAndWrite write = new StartConfigReadAndWrite();
                _ConfigList = await write.Reads();
            });

            Selected = new RelayCommand<StartConfigJson>((args) =>
            {
                if(args.Config.GamePath != null)
                {
                    App.Json = args;
                    _StartEnable = true;
                }
                else
                {
                    _StartEnable = false;
                }
            });

        }



        private bool StartEnable;

        public bool _StartEnable
        {
            get { return StartEnable; }
            set 
            {
                SetProperty(ref StartEnable, value);
            }
        }


        public RelayCommand Loaded { get; private set; }

        private ObservableCollection<StartConfigJson> ConfigList;

        public ObservableCollection<StartConfigJson> _ConfigList
        {
            get { return ConfigList; }
            set 
            {
                SetProperty(ref ConfigList, value);

            }
        }


        private async Task start()
        {
            if (_StartEnable == false || App.Json == null)
            {
                TipWindow.Show("启动失败，未设置启动配置", "😒");
                return;
            }
            StartGame startAgument = new StartGame();
            string a = await startAgument.GO(App.Json, () => NotificationHelper.Show("应用隐藏", "可以双击任务栏托盘图标进行重新打开"));
            if (a == "1")
            {
                TipWindow.Show("启动游戏成功！", "可以快乐的玩耍了");
            }
            else
            {

                TipWindow.Show("启动失败，请检查游戏文件夹！", "😒");
            };
        }

        public RelayCommand StartGame { get; private set; }

        public RelayCommand<StartConfigJson> Selected { get; private set; }
        public RelayCommand NewConfig { get; private set; } = new RelayCommand(() =>
        {
            NavigationHelper helper = new NavigationHelper();
            INavigations navigations = new INavigations();
            navigations.MyAction = () =>
            {
                (App.MainWindow.Content as MainPage).MyFrame.Navigate(typeof(NewStartConfig));

            };
            navigations.Message = "跳转到配置页";
            helper.GO(navigations);
        });
    }
}
