using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProviders;

namespace UIWindows
{
    public class TrayService : ITrayService
    {
        WindowsTrayIcon tray;

        public Action ClickHandler { get; set; }

        public void Initialize()
        {
            tray = new WindowsTrayIcon("Platforms/Windows/app.ico");
            tray.LeftClick = () => {
                WindowExtensions.BringToFront();
                ClickHandler?.Invoke();
            };
        }
    }
}
