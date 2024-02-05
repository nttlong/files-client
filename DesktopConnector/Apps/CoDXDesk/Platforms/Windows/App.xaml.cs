using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CoDXDesk.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
        //private void SetupTrayIcon()
        //{
        //    var trayService = ServiceProvider.GetService<ITrayService>();

        //    if (trayService != null)
        //    {
        //        trayService.Initialize();
        //        trayService.ClickHandler = () =>
        //            ServiceProvider.GetService<INotificationService>()
        //                ?.ShowNotification("Hello Build! 😻 From .NET MAUI", "How's your weather?  It's sunny where we are 🌞");
        //    }
        //}
    }

}
