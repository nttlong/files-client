using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using CodxDesk.Services;
#if WINDOWS
using CodxDesk.WinUI.Platforms.Windows;
#endif
namespace CodxDesk
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
//    public static class MauiProgram
//    {
//        public static MauiApp CreateMauiApp()
//        {
//            var builder = MauiApp.CreateBuilder();
//            builder
//                .UseMauiApp<App>()
//                .ConfigureFonts(fonts =>
//                {
//                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
//                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
//                });
////            builder.ConfigureLifecycleEvents(lifecycle => {
////#if WINDOWS

////                //lifecycle
////                //    .AddWindows(windows =>
////                //        windows.OnPlatformMessage((app, args) =>
////                //        {
////                //            if (CodxDeskWindowExtensions.Hwnd == IntPtr.Zero)
////                //            {
////                //                CodxDeskWindowExtensions.Hwnd = args.Hwnd;
////                //                CodxDeskWindowExtensions.SetIcon("Platforms/Windows/trayicon.ico");
////                //            }
////                //        }));
////                lifecycle.AddWindows(windows => windows.OnWindowCreated((del) =>
////                {
////                    del.ExtendsContentIntoTitleBar = true;
////                }));
////#endif
////            });

//            var services = builder.Services;
//#if WINDOWS

//            services.AddSingleton<ITrayService, TrayService>();
//            services.AddSingleton<INotificationService, NotificationService>();
//#elif MACCATALYST
//            //services.AddSingleton<ITrayService, TrayService>();
//            //services.AddSingleton<INotificationService, MacCatalyst.NotificationService>();
//#endif
//#if DEBUG
//            builder.Logging.AddDebug();
//#endif

//            return builder.Build();
//        }
//    }
}
