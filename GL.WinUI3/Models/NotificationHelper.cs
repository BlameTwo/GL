using CommunityToolkit.WinUI.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp1.Models
{
    public static class NotificationHelper
    {
        public static void Show(string toastHeading,string toastBody)
        {
            var value = new ToastContentBuilder().AddText(toastHeading).AddText(toastBody);
            value.Show();
        }
    }
}
