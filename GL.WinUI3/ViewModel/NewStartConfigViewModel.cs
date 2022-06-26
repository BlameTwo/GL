using GL.WinUI3.EventArgs;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using MyApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            _Lists = new ObservableCollection<ExeConfig>();
        }

        public void Receive(ExeArgs message)
        {
            if (Exit(message.Config) == false)
            {
                if(message.ExeEnum == ExeEnum.Add)
                    _Lists.Add(message.Config);
            }
            if(message.ExeEnum == ExeEnum.Remove)
            {
                _Lists.Remove(message.Config);
            }
            
        }

        bool Exit(ExeConfig exeConfig)
        {
            foreach (var item in _Lists)
            {
                if(exeConfig.Path == item.Path)
                {
                    return true;
                }
            }
            return false;
        }

        private ObservableCollection<ExeConfig> Lists;

        public ObservableCollection<ExeConfig> _Lists
        {
            get { return Lists; }
            set => SetProperty(ref Lists, value);
        }


    }
}
