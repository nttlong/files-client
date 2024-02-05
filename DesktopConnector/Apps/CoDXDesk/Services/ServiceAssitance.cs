using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoDXDesk.Services
{
    public class ServiceAssistent
    {
        public static TService GetService<TService>()
        {
            var current = GetCurrent();
            return ((TService)current.GetService(typeof(TService)));
        }

        public static IServiceProvider GetCurrent()
        {
#if WINDOWS10_0_17763_0_OR_GREATER
            return MauiWinUIApplication.Current.Services;
#elif ANDROID
            return MauiApplication.Current.Services;
#elif IOS || MACCATALYST
			return MauiUIApplicationDelegate.Current.Services;
#else
			return null;
#endif
            //return null;
        }
    }
}
