using CodxClient.Services;
using Microsoft.Maui.Controls;

namespace CodxClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //SetupTrayIcon();

        }
        
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    this.MaximumHeightRequest
        //    if (NavigationPage.CurrentPage is MyContentPage page)
        //    {
        //        NavigationPage.Width = 500;
        //        NavigationPage.Height = 500;
        //    }
        //}
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
