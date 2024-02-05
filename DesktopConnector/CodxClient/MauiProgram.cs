using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using CodxClient.Libs;
using Windows.Graphics;

namespace CodxClient
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
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
                });
            builder.ConfigureLifecycleEvents(lifecycle =>
            {
                Services.ServiceAssistent.GetService<Services.IServer>().RunAsync("ws://127.0.0.1:8765").Start();
                var trayService = Services.ServiceAssistent.GetService<Services.ITrayService>();
                var uiService = Services.ServiceAssistent.GetService<Services.IUIService>();
                var stathsuOfMainWiwndow = false;
                if (trayService != null)
                {
                    trayService.Initialize();

                }
#if WINDOWS
                lifecycle.AddWindows(windows => windows.OnWindowCreated((del) =>
                {
                SizeInt32 size = new SizeInt32
                {
                    Height=500,
                    Width=500,
                };
                    del.GetAppWindow().Resize(size);
                    del.GetAppWindow().Closing += (s, e) =>
                    {
                        e.Cancel= true;
                        uiService.HidePage(uiService.GetHomePage());
                    };
                    del.GetAppWindow().Changed += (s, e) =>
                    {
                        if(e.DidSizeChange)
                        {
                            if (s.Size.Height < 50)
                            {
                                if (!stathsuOfMainWiwndow)
                                {
                                    uiService.HidePage(uiService.GetHomePage());
                                    stathsuOfMainWiwndow = true;
                                }
                                else
                                {
                                    uiService.ShowPage(uiService.GetHomePage());
                                    stathsuOfMainWiwndow = false;
                                }
                            }
                        }
                    };


                    del.ExtendsContentIntoTitleBar = true;
                    
                }));

#endif
            });

            var services = builder.Services;
#if WINDOWS
            services.AddSingleton<Services.IServer, ServiceFactory.Server>();
            services.AddSingleton<Services.ITrayService, ServiceFactory.TrayService>();
            services.AddSingleton<Services.INotificationService, ServiceFactory.NotificationService>();
            services.AddSingleton<Services.ILoggingService, ServiceFactory.LoggingService>();
            services.AddSingleton<Services.IConfigService, ServiceFactory.ConfigService>();
            services.AddSingleton<Services.IContentService, ServiceFactory.ContentService>();
            services.AddSingleton<Services.IOfficeService, ServiceFactory.OfficeService>();
            services.AddSingleton<Services.IUIService, ServiceFactory.UIService>();
            services.AddSingleton<Services.IBackgroundService, ServiceFactory.BackgroundService>();
#elif MACCATALYST
            services.AddSingleton<Services.IServer, ServiceFactory.Server>();
            services.AddSingleton<Services.ITrayService, ServiceFactory.TrayService>();
            services.AddSingleton<Services.INotificationService, ServiceFactory.NotificationService>();
            services.AddSingleton<Services.ILoggingService, ServiceFactory.LoggingService>();
            services.AddSingleton<Services.IConfigService, ServiceFactory.ConfigService>();
            services.AddSingleton<Services.IContentService, ServiceFactory.ContentService>();
            services.AddSingleton<Services.IOfficeService, ServiceFactory.OfficeService>();
            services.AddSingleton<Services.IUIService, ServiceFactory.UIService>();
            services.AddSingleton<Services.IBackgroundService, ServiceFactory.BackgroundService>();
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
