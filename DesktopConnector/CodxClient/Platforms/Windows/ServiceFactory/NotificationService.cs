using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;

namespace CodxClient.ServiceFactory
{
    public class NotificationService : Services.INotificationService
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
