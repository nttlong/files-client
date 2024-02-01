using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProviders;
using Microsoft.Toolkit.Uwp.Notifications;
namespace UIWindows
{
    public class NotificationService : INotificationService
    {
        public void ShowNotification(string title, string body)
        {
#if WINDOWS_UWP
            var ret = ToastContentBuilder()
               .AddToastActivationInfo(null, ToastActivationType.Foreground)
               .AddAppLogoOverride(new Uri("ms-appx:///Assets/dotnet_bot.png"))
               .AddText(title, hintStyle: AdaptiveTextStyle.Header)
               .AddText(body, hintStyle: AdaptiveTextStyle.Body);

                ret.Show();
#endif
        }
    }
}
