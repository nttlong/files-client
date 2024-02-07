using CodxClient.Services;
using Microsoft.Maui.Controls;
using System.Globalization;

namespace CodxClient
{
    public partial class AppShell : Shell
    {
        private IProcessService processService;

        public AppShell()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            this.processService= ServiceAssistent.GetService<Services.IProcessService>();
            //SetupTrayIcon();

        }
        protected override void OnDisappearing()
        {
            this.processService.ClearAll();
            base.OnDisappearing();
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
