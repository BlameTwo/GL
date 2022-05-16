using GenshinImpact_Lanucher.GameNotifys;
using GenshinImpact_Lanucher.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinImpact_Lanucher.ViewModels
{
    public class NotifyGamePageVM: ObservableRecipient
    {
        public NotifyGamePageVM()
        {
            IsActive = true;
            Loaded = new RelayCommand(() => load());
            RefNotify = new RelayCommand(() => load());
        }

        private ObservableCollection<Notice> NotiGame;

        public ObservableCollection<Notice> _NotiGame
        {
            get => NotiGame;
            set => SetProperty(ref NotiGame, value);
        }

        private ObservableCollection<Notice> NotiMiHaYo;

        public ObservableCollection<Notice> _NotiMiHaYo
        {
            get => NotiMiHaYo;
            set => SetProperty(ref NotiMiHaYo, value);
        }


        private async void load() 
        {
            _NotiGame = await GameNitify.GetOneAsync();
            _NotiMiHaYo = await GameNitify.GetTwoAsync();
            if (NotiGame == null || _NotiMiHaYo == null)
            {
                (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Message = "旅行者似乎网络出现错误了呢";
                (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Icon = WPFUI.Common.SymbolRegular.ErrorCircle24;
                (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Title = "公告加载失败";
                (System.Windows.Application.Current.MainWindow as MainWindow).WindowTitler.Show();
            }
        }
        public RelayCommand RefNotify { get; set; }
        public RelayCommand Loaded { get; set; }
    }
}
