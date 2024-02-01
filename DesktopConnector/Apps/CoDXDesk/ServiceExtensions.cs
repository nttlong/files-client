//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace CoDXDesk
//{
//    public static class ServiceProviderExt
//    {
//        public static TService GetService<TService>(this ServiceProvider context)
//        {
//            var current = context.GetCurrent();
//            return ((TService)current.GetService(typeof(TService)));
//        }

//        public static IServiceProvider GetCurrent(this ServiceProvider context)
//        {
//#if WINDOWS10_0_17763_0_OR_GREATER
//            return MauiWinUIApplication.Current.Services;
//#elif ANDROID
//            MauiApplication.Current.Services;
//#elif IOS || MACCATALYST
//			return MauiUIApplicationDelegate.Current.Services;
//#else
//			return null;
//#endif
//        }
//    }
        
//}
