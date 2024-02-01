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
#if WINDOWS
                //lifecycle
                //    .AddWindows(windows =>
                //        windows.OnNativeMessage((app, args) => {
                //            if (WindowExtensions.Hwnd == IntPtr.Zero)
                //            {
                //                WindowExtensions.Hwnd = args.Hwnd;
                //                WindowExtensions.SetIcon("Platforms/Windows/trayicon.ico");
                //            }
                //        }));
                
                lifecycle.AddWindows(windows => windows.OnWindowCreated((del) => {
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
            


            return builder.Build();
        }
    }
}
