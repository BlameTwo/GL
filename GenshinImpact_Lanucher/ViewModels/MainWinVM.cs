using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinImpact_Lanucher.ViewModels
{
    public class MainWinVM: ObservableRecipient
    {
        public MainWinVM()
        {
            Loaded = new RelayCommand(() => loaded());

        }

        private void loaded()
        {

        }

        public RelayCommand Loaded { get; private set; }
    }
}
