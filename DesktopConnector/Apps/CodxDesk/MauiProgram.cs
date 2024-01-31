using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using XNotificationService;
using XNotificationServiceWindows;
namespace CodxDesk
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var url = new Uri("ms-appx:///Resources/AppIcon/appicon.svg");
            var fx = url.AbsolutePath;
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
                });
            builder.ConfigureLifecycleEvents(lifecycle => {
#if WINDOWS

                
                lifecycle.AddWindows(windows => windows.OnWindowCreated((del) => {
                    del.ExtendsContentIntoTitleBar = true;
                    //XWindows.XWindowsExtension.Hwnd= ((Microsoft.Maui.MauiWinUIWindow)del).WindowHandle;
                    //XWindows.XWindowsExtension.SetIcon(@"C:\Users\admin.NTTLONG\source\repos\files-client\DesktopConnector\Apps\CodxDesk\Resources\AppIcon\trayicon.ico");
                    //XWindows.XWindowsExtension.MinimizeToTray();
                }));
#endif
            });

            var services = builder.Services;
#if WINDOWS

            services.AddSingleton<ITrayService, TrayService>();
            services.AddSingleton<INotificationService, NotificationService>();
            //#elif MACCATALYST
            //            services.AddSingleton<ITrayService, TrayService>();
            //            services.AddSingleton<INotificationService, MacCatalyst.NotificationService>();
#endif
            //services.AddSingleton<HomeViewModel>();
            //services.AddSingleton<HomePage>();




#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
