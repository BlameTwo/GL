using GenshinImpact_Lanucher.MiHaYouAPI;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            });
        }


        private MiHaYouAccountArgs MyAccount;

        public MiHaYouAccountArgs _MyAccount
        {
            get => MyAccount;
            set =>SetProperty(ref MyAccount, value);    
        }
        public RelayCommand Loaded { get; private set; }
    }
}
