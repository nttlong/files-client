using Microsoft.Extensions.Logging;
using UIImplements;
using UIProviders;
using Microsoft.Maui.LifecycleEvents;
using System.Xml;
using CodxDesk;
namespace CoDXDesk
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
                });
            builder.ConfigureLifecycleEvents(lifecycle => {
                ServiceAssistent.GetService<IServer>().RunAsync("ws://127.0.0.1:8765").Start();
#if WINDOWS
                lifecycle.AddWindows(windows => windows.OnWindowCreated((del) => {
                    var hwnd= ((Microsoft.Maui.MauiWinUIWindow)del).WindowHandle;
                    
                    WinApi.SendMessage(hwnd, WinApi.WM_SYSCOMMAND, new IntPtr(WinApi.SC_MINIMIZE), IntPtr.Zero);
                    del.ExtendsContentIntoTitleBar = true;
                }));

#endif
            });

            var services = builder.Services;
#if WINDOWS
            services.AddSingleton<IServer, Server>();
            services.AddSingleton<ITrayService, TrayService>();
            services.AddSingleton<INotificationService, NotificationService>();
#elif MACCATALYST
            services.AddSingleton<ITrayService, TrayService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<IServer, Server>();
#endif

#if DEBUG
            builder.Logging.AddDebug();
#endif

//#if WINDOWS
//            var fx = new UIImplements.Server();
//            fx.RunAsync().Start();
//#endif
            
            return builder.Build();
        }
    }
}
