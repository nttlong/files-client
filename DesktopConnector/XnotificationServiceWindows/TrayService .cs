using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNotificationService;
using XWindows;


namespace XNotificationServiceWindows
{
    public class TrayService : ITrayService
    {
        WindowsTrayIcon tray;

        public Action ClickHandler { get; set; }

        public void Initialize()
        {
            tray = new WindowsTrayIcon("Platforms/Windows/trayicon.ico");
            tray.LeftClick = () => {
                XWindowsExtension.BringToFront();
                ClickHandler?.Invoke();
            };
        }
    }
}
