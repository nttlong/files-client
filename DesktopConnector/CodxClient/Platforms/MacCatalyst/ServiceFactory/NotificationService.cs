using CodxClient.Models;
using CodxClient.Models.Exceptions;
using UserNotifications;


namespace CodxClient.ServiceFactory
{

    public class NotificationService : Services.INotificationService
    {
        public Task<bool> ShowConfirmBoxAsync(string Title, string ConfirmMessage)
        {
            throw new NotImplementedException();
        }

        public void ShowError(RequestError ex)
        {
            throw new NotImplementedException();
        }

        public void ShowNotification(string title, string body, bool silent)
        {
            throw new NotImplementedException();
        }

        public object ShowNotificationWithWithProgressBar(string title, string body, bool silent)
        {
            throw new NotImplementedException();
        }

        public object ShowNotificationWithWithProgressBar(string title, string body, string status, bool silent)
        {
            throw new NotImplementedException();
        }

        public void UpdateNotifier(object notifier, string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}