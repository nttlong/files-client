using CodxDesk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoDXDesk.ServiceFactory
{
    public class TrayService : Services.ITrayService
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
