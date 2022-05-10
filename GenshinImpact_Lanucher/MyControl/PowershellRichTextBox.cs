using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace GenshinImpact_Lanucher.MyControl
{
    public class PowershellRichTextBox:RichTextBox
    {
        static PowershellRichTextBox tb;
        /// <summary>
        /// 行高
        /// </summary>
        public double LineHeight
        {
            get { return (double)GetValue(LineHeightProperty); }
            set { SetValue(LineHeightProperty, value); }
        }
        public static readonly DependencyProperty LineHeightProperty =
          DependencyProperty.Register("LineHeight", typeof(double), typeof(PowershellRichTextBox), new PropertyMetadata(null));

        private static void GetParagraph(string Htmlstring)
        {
            Paragraph para = new Paragraph
            {
                Margin = new Thickness(0) // remove indent between paragraphs
            };
            para.Inlines.Add(Htmlstring);
            //将para的LineHeight和自定义LineHeight绑定
            var binding = new Binding("LineHeight");
            binding.Source = tb;
            para.SetBinding(Paragraph.LineHeightProperty, binding);
            tb.Document.Blocks.Add(para);
        }
    }
}
