using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.WinUI3.ViewModel
{
    public class MainPageViewModel:ObservableRecipient
    {
        public MainPageViewModel()
        {
            IsActive = true;
        }
    }
}
