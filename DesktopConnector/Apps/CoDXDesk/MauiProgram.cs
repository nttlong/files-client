using Microsoft.Extensions.Logging;
using UIImplements;
using UIProviders;
using Microsoft.Maui.LifecycleEvents;
using System.Xml;
using CodxDesk;
using Microsoft.Maui.Controls.PlatformConfiguration;

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
                    del.ExtendsContentIntoTitleBar = true;
                }));

#endif
            });

            var services = builder.Services;
#if WINDOWS
            services.AddSingleton<IServer, Server>();
            services.AddSingleton<ITrayService, TrayService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<ILoggingService,LoggingService>();
            services.AddSingleton<IConfigService,ConfigService>();
            services.AddSingleton<IContentService,ContentService>();
            services.AddSingleton<IWordService,WordService>();
#elif MACCATALYST
            services.AddSingleton<ITrayService, TrayService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<IServer, Server>();
            services.AddSingleton<ILoggingService,LoggingService>();
            services.AddSingleton<IConfigService,ConfigService>();
            services.AddSingleton<IContentService,ContentService>();
            services.AddSingleton<IWordService,WordService>();
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
