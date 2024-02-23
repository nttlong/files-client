using DeskCnn.Services;
using Microsoft.Maui.Controls;
using System.Globalization;

namespace DeskCnn
{
    public partial class AppShell : Shell
    {
        //const string HasRunKey = "HasRunKey";
        private IProcessService processService;
        private INotificationService notifcationService;

        public AppShell()
        {
            InitializeComponent();
            
            this.processService= ServiceAssistent.GetService<Services.IProcessService>();
            this.notifcationService = ServiceAssistent.GetService<Services.INotificationService>();
            //SetupTrayIcon();
            //bool hasRunBefore = Preferences.Get(HasRunKey, false);
            //if(hasRunBefore)
            //{
            //    this.notifcationService.ShowNotification("You!", "Please, do not run twice.");
            //}
            //else
            //{
            //    Preferences.Set(HasRunKey, true);
            //}

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
