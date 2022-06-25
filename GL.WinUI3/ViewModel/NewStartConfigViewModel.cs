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
        }

        public void Receive(ExeArgs message)
        {
            switch (message.ExeEnum)
            {
                case ExeEnum.Add:
                    _Lists.Add(message.Config);
                    break;
                case ExeEnum.Remove:
                    _Lists.Remove(message.Config);
                    break;
            }
        }


        private ObservableCollection<ExeConfig> Lists;

        public ObservableCollection<ExeConfig> _Lists
        {
            get { return Lists; }
            set => SetProperty(ref Lists, value);
        }


    }
}
