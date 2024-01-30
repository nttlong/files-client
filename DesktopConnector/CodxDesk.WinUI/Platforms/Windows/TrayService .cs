using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodxDesk.Services;
using Hardcodet.Wpf.TaskbarNotification.Interop;
using Microsoft.UI.Xaml;

namespace CodxDesk.WinUI.Platforms.Windows
{
    public class TrayService : ITrayService
    {
        WindowsTrayIcon tray;

        public Action ClickHandler { get; set; }

        public void Initialize()
        {
            tray = new WindowsTrayIcon("Platforms/Windows/trayicon.ico");
            tray.LeftClick = () => {
                WindowExtensions.BringToFront();
                ClickHandler?.Invoke();
            };
        }
    }
}
