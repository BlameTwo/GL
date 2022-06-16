using GenshinImpact_Lanuncher.GameNotifys;
using GenshinImpact_Lanuncher.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenshinImpact_Lanuncher.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace GenshinImpact_Lanuncher.ViewModels
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
                TipWindow.Show("公告加载失败", "旅行者似乎网络出现错误了呢", WPFUI.Common.SymbolRegular.ErrorCircle24);

            }
        }
        public RelayCommand RefNotify { get; set; }
        public RelayCommand Loaded { get; set; }
    }
}
