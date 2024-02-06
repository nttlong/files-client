using CodxClient.Services;
using Microsoft.Maui.Controls;
using System.Globalization;

namespace CodxClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //SetupTrayIcon();

        }

        private void CloseBtn_Clicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
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
