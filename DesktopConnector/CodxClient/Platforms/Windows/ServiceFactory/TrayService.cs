
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodxClient.ServiceFactory
{
    public class TrayService : Services.ITrayService
    {
        Libs.WindowsTrayIcon tray;

        public Action ClickHandler { get; set; }

        public void Initialize()
        {
            tray = new Libs.WindowsTrayIcon("Platforms/Windows/app.ico");
            tray.LeftClick = () => {
                Libs.WindowExtensions.BringToFront();
                ClickHandler?.Invoke();
            };
        }
    }
}
