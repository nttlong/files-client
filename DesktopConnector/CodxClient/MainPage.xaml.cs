
//using CodxClient.Libs;
using CodxClient.Services;
using System.ComponentModel;
using Microsoft.Maui.Controls;
using CodxClient.Models;
using System.Reflection;

namespace CodxClient
{
    public partial class MainPage : ContentPage
    {
        
        private IUIService uiService;
        private IBackgroundService backgroundService;
        private IToolDetectorService toolDetector;
        private INotificationService notificationService;
        private IConfigService configService;

        public MainPage()
        {
            InitializeComponent();
            this.uiService = ServiceAssistent.GetService<IUIService>();
            this.uiService.SetHomePage(this);
            this.toolDetector = ServiceAssistent.GetService<IToolDetectorService>();
            this.OfficeToolsData = this.toolDetector.DoDetectOffice();
            this.OfficeToolsList.ItemsSource = this.OfficeToolsData;
            this.notificationService = ServiceAssistent.GetService<INotificationService>();
            this.configService = ServiceAssistent.GetService<IConfigService>();
            
            
            
        }

        public IList<OfficeTools> OfficeToolsData { get; private set; }
        
        public bool IsAutoStartUp { get;  set; }

        private void OnClickAutoStartUpLabel(object sender, EventArgs e)
        {
            this.IsAutoStartUp = !IsAutoStartUp;
            this.chkAutoStartUp.IsChecked = this.IsAutoStartUp;
            //this.confirmAutoStart();
        }
        private void OnChkAutoStartUpChanged(object s, CheckedChangedEventArgs e)
        {
            this.IsAutoStartUp = e.Value;
            this.configService.SetAutoStartUp(this.IsAutoStartUp);
            //this.chkAutoStartUp.IsChecked = this.IsAutoStartUp;
        }

        private void confirmAutoStart()
        {
            App.Current.MainPage.DisplayAlert("??", "Are you sure?","OK","Cancel");
            //var ret= this.notificationService.ShowConfirmBoxAsync("??", "Are you sure?").Result;
        }

        private void CloseBtn_Clicked(object sender, EventArgs e)
        {
            Application.Current.Quit(); // Exit the application

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
