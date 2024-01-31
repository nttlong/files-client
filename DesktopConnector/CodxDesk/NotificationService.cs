using Microsoft.Toolkit.Uwp.Notifications;
//using WeatherTwentyOne.Services;
using CodxDesk.Services;
namespace CodxDesk
{

    public class NotificationService : INotificationService
    {
        public void ShowNotification(string title, string body) => new ToastContentBuilder()
                .AddToastActivationInfo(null, ToastActivationType.Foreground)
                .AddAppLogoOverride(new Uri("ms-appx:///Assets/WP.ico"))
                .AddText(title, hintStyle: AdaptiveTextStyle.Header)
                .AddText(body, hintStyle: AdaptiveTextStyle.Body)
                .Show();
    }
}