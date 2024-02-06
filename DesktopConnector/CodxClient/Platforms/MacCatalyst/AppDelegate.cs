using Foundation;
using UIKit;
using Microsoft.Maui.Controls;
namespace CodxClient
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
        public override bool WillFinishLaunching(UIApplication application, NSDictionary launchOptions)
        {
            //var fx = application.Windows[0];
            return base.WillFinishLaunching(application, launchOptions);
            
        }
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            return base.FinishedLaunching(application, launchOptions);
        }
        public override void WillTerminate(UIApplication application)
        {
            base.WillTerminate(application);
            
        }
    }
}
