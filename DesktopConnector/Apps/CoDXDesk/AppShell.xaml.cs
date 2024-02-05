

namespace CoDXDesk
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            SetupTrayIcon();
            
        }
        protected override async void OnDisappearing()
        {
            var result = await DisplayAlert("Unsaved Changes", "Do you want to save changes?", "Save", "Discard");
        }
        private void SetupTrayIcon()
        {

            var trayService = Services.ServiceAssistent.GetService<Services.ITrayService>();
            
            if (trayService != null)
            {
                trayService.Initialize();
                trayService.ClickHandler = () =>
                    Services.ServiceAssistent.GetService<Services.INotificationService>()
                        ?.ShowNotification("XXX", "YYY");
            }
            
        }
    }
}
