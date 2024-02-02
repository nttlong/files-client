using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProviders;
using Microsoft.Toolkit.Uwp.Notifications;

namespace UIImplements
{
    public class NotificationService : INotificationService
    {
        public void ShowNotification(string title,string body)
        {
            new ToastContentBuilder()
            .AddText(title)
            .AddText(body)
            .Show();
            
        }
    }
}
