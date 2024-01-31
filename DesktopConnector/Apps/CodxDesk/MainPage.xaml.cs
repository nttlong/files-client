using Microsoft.Extensions.DependencyInjection;
using XNotificationService;
using Microsoft.Extensions.DependencyInjection;
using XNotificationServiceWindows;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace CodxDesk
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private INotificationService mySvc;
        private ServiceProvider serviceProvider;
        private INotificationService _myService;
        private ITrayService _trayIcon;

        public MainPage()
        {
            InitializeComponent();
            this.serviceProvider = new ServiceCollection()
                .AddSingleton<XNotificationService.INotificationService, XNotificationServiceWindows.NotificationService>()
                .AddSingleton< XNotificationService.ITrayService,XNotificationServiceWindows.TrayService>()
                .BuildServiceProvider();

            this._myService = serviceProvider.GetRequiredService<XNotificationService.INotificationService>();
            this._trayIcon = serviceProvider.GetRequiredService<XNotificationService.ITrayService>();
            this._trayIcon.Start();
            //XWindows.XWindowsExtension.SetIcon(@"C:\Users\admin.NTTLONG\source\repos\files-client\DesktopConnector\Apps\CodxDesk\Resources\AppIcon\trayicon.ico");
            //XWindows.XWindowsExtension.MinimizeToTray();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            XWindows.XWindowsExtension.SetIcon(@"C:\Users\admin.NTTLONG\source\repos\files-client\DesktopConnector\Apps\CodxDesk\Resources\AppIcon\trayicon.ico");
            XWindows.XWindowsExtension.MinimizeToTray();
            //XWindows.XWindowsExtension.BringToFront();
            var appHandler = ((Microsoft.Maui.Controls.VisualElement)((Microsoft.Maui.Controls.Element)sender).Parent).Window.RealParent.Handler;
            count++;
            this._myService.ShowNotification("hello", "OK");
            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
