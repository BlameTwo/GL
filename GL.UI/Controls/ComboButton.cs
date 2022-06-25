using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.UI.Controls
{
    public class ComboButtonControl: Expander
    {
        public ComboButtonControl()
        {
            DefaultStyleKey = typeof(ComboButton);
        }



        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ComboButton), new PropertyMetadata(null));


    }
}
