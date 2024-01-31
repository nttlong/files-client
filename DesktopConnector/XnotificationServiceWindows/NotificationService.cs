using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNotificationService;
using XWindows;
using Microsoft.Toolkit.Uwp.Notifications;
namespace XNotificationServiceWindows
{
    public class NotificationService : INotificationService
    {
        public void ShowNotification(string title, string body)
        {
            var ret = new ToastContentBuilder()
                .AddToastActivationInfo(null, ToastActivationType.Foreground)
                .AddAppLogoOverride(new Uri("ms-appx:///Assets/WP.ico"))
                .AddText(title, hintStyle: AdaptiveTextStyle.Header)
                .AddText(body, hintStyle: AdaptiveTextStyle.Body);
#if WINDOWS
            ret.Show();
#endif

        }
    }
}
