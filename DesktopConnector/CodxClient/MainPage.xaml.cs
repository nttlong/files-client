
//using CodxClient.Libs;
using CodxClient.Services;
using System.ComponentModel;

namespace CodxClient
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private IUIService uiService;
        private IBackgroundService backgroundService;

        public MainPage()
        {
            InitializeComponent();
            this.uiService = ServiceAssistent.GetService<IUIService>();
            this.uiService.SetHomePage(this);
            //this.backgroundService = ServiceAssistent.GetService<IBackgroundService>();
            //this.IsVisible = false;
        }
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    // Subscribe to the BackPressed event
        //    BackPressed += OnBackPressed;
        //}

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();

        //    // Unsubscribe from the BackPressed event when the page disappears
        //    BackPressed -= OnBackPressed;
        //}

        private void OnBackPressed(object sender, EventArgs e)
        {
            // Prevent the application from exiting
            // You can add your own logic here if needed
            // For example, show a confirmation dialog before exiting
        }
        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    base.OnClosing(e);

        //    // Prompt for confirmation or perform other actions
        //    e.Cancel = true; // Prevent closing by default

        //    // Example: Show a confirmation dialog
        //    if (await DisplayAlert("Exit Confirmation", "Are you sure you want to exit?", "Yes", "No"))
        //    {
        //        e.Cancel = false; // Allow closing if confirmed
        //    }
        //}

        //protected override void ChangeVisualState()
        //{
        //    if (this.Window != null)
        //    {
        //        ServiceAssistent.GetService<IUIService>().HidePage(this);
        //    }
        //    this.uiService.SetHomePage(this);
        //    //var mp = this.uiService.GetHomePage();
        //    //if (mp == null)
        //    //{
        //    //    ServiceAssistent.GetService<IUIService>().HidePage(this);
        //    //    this.uiService.SetHomePage(this);
        //    //}
        //    //var hwnd = ((Microsoft.Maui.MauiWinUIWindow)((Microsoft.Maui.Handlers.ElementHandler<Microsoft.Maui.IWindow, Microsoft.UI.Xaml.Window>)((Microsoft.Maui.Controls.Element)((Microsoft.Maui.Controls.BaseShellItem)this.Parent.Parent).Window.Handler.VirtualView).Handler).PlatformView).WindowHandle;

        //}
        //private void StartBackgroundService()
        //{
        //    backgroundService.Start();
        //}

        //private void StopBackgroundService()
        //{
        //    backgroundService.Stop();
        //}
    }

}
