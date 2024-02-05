
using CodxClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodxClient.ServiceFactory
{
    public class TrayService : Services.ITrayService
    {
        private IUIService uiSrvice;
        Libs.WindowsTrayIcon tray;

        public Action ClickHandler { get; set; }
        

        public void Initialize()
        {
            this.uiSrvice = ServiceAssistent.GetService<IUIService>();
            tray = new Libs.WindowsTrayIcon("Platforms/Windows/app.ico");
            
            tray.LeftClick = () => {
                ServiceAssistent.GetService<IUIService>().Show();
                //var app = new TrayIconPage();
                //var navigationPage = new NavigationPage(this.uiSrvice.GetHomePage());
                //navigationPage.PushAsync(app).Wait();
                //this.uiSrvice.ShowPage(app);
            };
        }
    }
}
