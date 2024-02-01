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
            //var ret = ToastContentBuilder.CreateProgressBarData
            //   .AddToastActivationInfo(null, ToastActivationType.Foreground)
            //   .AddAppLogoOverride(new Uri("ms-appx:///Assets/dotnet_bot.png"))
            //   .AddText(title, hintStyle: AdaptiveTextStyle.Header)
            //   .AddText(body, hintStyle: AdaptiveTextStyle.Body);
            //#if WINDOWS_UWP


//            ret.Show();
//#endif
        }
    }
}
