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

        private void SetupTrayIcon()
        {

            var trayService = ServiceAssistent.GetService<ITrayService>();

            if (trayService != null)
            {
                trayService.Initialize();
                trayService.ClickHandler = () =>
                    ServiceAssistent.GetService<INotificationService>()
                        ?.ShowNotification("Hello Build! 😻 From .NET MAUI", "How's your weather?  It's sunny where we are 🌞");
            }
        }
    }
}
