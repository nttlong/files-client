using UIProviders;
using UIImplements;
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

            var trayService = ServiceAssistent.GetService<ITrayService>();

            if (trayService != null)
            {
                trayService.Initialize();
                trayService.ClickHandler = () =>
                    ServiceAssistent.GetService<INotificationService>()
                        ?.ShowNotification("XXX", "YYY");
            }
        }
    }
}
