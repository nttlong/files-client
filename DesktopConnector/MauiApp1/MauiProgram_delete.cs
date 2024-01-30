using CodxDesk.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
#if WINDOWS
using CodxDesk.WinUI.Platforms.Windows;
#endif
namespace MauiApp1
{
    public static class MauiProgram_delete
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
                

                lifecycle.AddWindows(windows => windows.OnWindowCreated((del) => {
                    del.ExtendsContentIntoTitleBar = true;
                }));
#endif
            });

            var services = builder.Services;
#if WINDOWS
            
            services.AddSingleton<ITrayService, TrayService>();
            services.AddSingleton<INotificationService, NotificationService>();
#elif MACCATALYST
            services.AddSingleton<ITrayService, TrayService>();
            services.AddSingleton<INotificationService, MacCatalyst.NotificationService>();
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
