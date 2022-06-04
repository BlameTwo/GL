
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

        public GenshinAccountArgs NowAccountArgs { get; private set; }
        public MainGenshinVM()
        {
            API.GetMiHaYouAccount();
            Loaded = new RelayCommand(async () =>
            {
                _MyAccount = await API.GetMiHaYouAccount();

                _GenshinAccounts = await API.GetGenshinAccount();

            });

            Sign = new RelayCommand(() => sign());

            _GetDay = new RelayCommand<GenshinAccountArgs>((args) =>getday(args));

            ValueChanged = new RelayCommand<ProgressBar>((pro) =>
            {
                currpro = pro;
            });

            ValueChanged2 = new RelayCommand<ProgressBar>((pro) =>
            {
                menoypro = pro;
            });

            ValueChanged3 = new RelayCommand<ProgressBar>((pro) =>
            {
                bosspro = pro;
            });

            ValueChanged4 = new RelayCommand<ProgressBar>((pro) =>
            {
                transoformer = pro;
            });
        }

        private async  void sign()
        {
             var list =  await API.GenshinSign(NowAccountArgs.Uid, NowAccountArgs.OwnerServer);
        }


        /// <summary>
        /// 脆弱数值的控件
        /// </summary>
        ProgressBar currpro = new ProgressBar();
        /// <summary>
        /// 洞天宝钱的控件
        /// </summary>
        ProgressBar menoypro = new ProgressBar();
        /// <summary>
        /// 周本减免
        /// </summary>
        ProgressBar bosspro = new ProgressBar();
        /// <summary>
        /// 质变仪
        /// </summary>
        ProgressBar transoformer = new ProgressBar();
        public async void getday(GenshinAccountArgs args)
        {
            if (args == null)
                return;
            _MyData = await  MiHaYouAPI.API.GenDay(args.OwnerServer, args.Uid);
            _MyGenshinMore = await API.GetGenshinMore(args.OwnerServer, args.Uid);
            NowAccountArgs = args;
            Ref(currpro, RefArgs.Current);
            Ref(menoypro, RefArgs.Menoy);
            Ref(bosspro, RefArgs.Boss);
            Ref(transoformer, RefArgs.transoformer);

            _Genshinmore = await API.GetGenshinMore(args.OwnerServer, args.Uid);
        }

        public enum RefArgs
        {
            Current,
            Menoy,
            Boss,
            transoformer
        }
        
        public void Ref(ProgressBar pro1, RefArgs arg)
        {
            ProgressBar pro = new ProgressBar();
            double to = 0;
            switch (arg)
            {
                case RefArgs.Current:
                    pro = currpro;
                    to = Double.Parse( _MyData.Current_resion);
                    break;
                case RefArgs.Menoy:
                    pro = menoypro;
                    to = Double.Parse(_MyData.home_money);
                    break;
                case RefArgs.Boss:
                    pro = bosspro;
                    to = double.Parse(_MyData.boss);
                    break;
                case RefArgs.transoformer:
                    pro = transoformer;
                    to = _MyData.transformertime;
                    break;
            }
            DoubleAnimation da = new DoubleAnimation() { From = 0, To =to};
            da.EasingFunction = new CubicEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            da.Duration = new Duration(new TimeSpan(0, 0, 0, 1, 500));
            pro.BeginAnimation(ProgressBar.ValueProperty, da);
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

        private GenshinMore MyGenshinMore;

        public GenshinMore _MyGenshinMore
        {
            get => MyGenshinMore;
            set => SetProperty(ref MyGenshinMore, value);
        }

        private GenshinMore Genshinmore;

        public GenshinMore _Genshinmore
        {
            get { return Genshinmore; }
            set => SetProperty(ref Genshinmore, value);
        }



        public RelayCommand Loaded { get; private set; }

        public RelayCommand<GenshinAccountArgs> _GetDay { get; private set; }
        public RelayCommand<ProgressBar> ValueChanged { get; private set; }
        public RelayCommand<ProgressBar> ValueChanged2 { get; set; }
        public RelayCommand<ProgressBar> ValueChanged3 { get; set; }
        public RelayCommand<ProgressBar> ValueChanged4 { get; set; }
        public RelayCommand Sign { get; set; }
        private ObservableCollection<GenshinAccountArgs> GenshinAccounts;
        
        public ObservableCollection<GenshinAccountArgs> _GenshinAccounts
        {
            get => GenshinAccounts;
            set =>SetProperty(ref GenshinAccounts, value);  
        }
    }
}
