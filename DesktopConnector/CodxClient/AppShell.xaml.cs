using CodxClient.Services;

namespace CodxClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //SetupTrayIcon();

        }
        
        //private void SetupTrayIcon()
        //{

        //    var trayService = Services.ServiceAssistent.GetService<Services.ITrayService>();

        //    if (trayService != null)
        //    {
        //        trayService.Initialize();
        //        trayService.ClickHandler = () =>
        //            Services.ServiceAssistent.GetService<Services.INotificationService>()
        //                ?.ShowNotification("XXX", "YYY");
        //    }

        //}
    }
}
