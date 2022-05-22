
using GenshinImpact_Lanucher.MiHaYouAPI;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace GenshinImpact_Lanucher.ViewModels
{
    public class MainGenshinVM: ObservableRecipient
    {
        public MainGenshinVM()
        {
            API.GetMiHaYouAccount();
            Loaded = new RelayCommand(async () =>
            {
                _MyAccount = await API.GetMiHaYouAccount();

                _GenshinAccounts = await API.GetGenshinAccount();
            });
            _GetDay = new RelayCommand<GenshinAccountArgs>((args) =>getday(args));

            ValueChanged = new RelayCommand<ProgressBar>((pro) =>
            {
                currpro = pro;
            });
        }


        /// <summary>
        /// 脆弱数值的控件
        /// </summary>
        ProgressBar currpro = new ProgressBar();

        public async void getday(GenshinAccountArgs args)
        {
            if (args == null)
                return;
            _MyData = await  MiHaYouAPI.API.GenDay(args.OwnerServer, args.Uid);
            Ref(currpro);
        }
        
        public void Ref(ProgressBar pro1)
        {
            DoubleAnimation da = new DoubleAnimation() { From = 0, To =double.Parse( MyData.Current_resion)};
            da.EasingFunction = new CubicEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            da.Duration = new Duration(new TimeSpan(0, 0, 0, 1, 500));
            pro1.BeginAnimation(ProgressBar.ValueProperty, da);
        }

        private GenshinDayArgs MyData;

        public GenshinDayArgs _MyData
        {
            get => MyData;
            set => SetProperty(ref MyData, value);
        }


        private MiHaYouAccountArgs MyAccount;

        public MiHaYouAccountArgs _MyAccount
        {
            get { return MyAccount; }
            set { MyAccount = value; OnPropertyChanged(); }
        }

        public RelayCommand Loaded { get; private set; }

        public RelayCommand<GenshinAccountArgs> _GetDay { get; private set; }
        public RelayCommand<ProgressBar> ValueChanged { get; private set; }

        private ObservableCollection<GenshinAccountArgs> GenshinAccounts;

        public ObservableCollection<GenshinAccountArgs> _GenshinAccounts
        {
            get => GenshinAccounts;
            set =>SetProperty(ref GenshinAccounts, value);  
        }
    }
}
