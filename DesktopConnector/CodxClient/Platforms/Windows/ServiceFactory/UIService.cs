using CodxClient.Libs;
using CodxClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.ServiceFactory
{
    public class UIService:IUIService
    {
        ContentPage? mainPage = null;

        public void DisableCloseButton(ContentPage mainPage)
        {
            var win = ((Microsoft.Maui.Controls.Window)mainPage.Window);
            var hwnd = ((Microsoft.Maui.MauiWinUIWindow)((Microsoft.Maui.Handlers.ElementHandler)win.Handler).PlatformView).WindowHandle;
            WinApi.HideCloseButton(hwnd);
        }

        public ContentPage? GetHomePage()
        {
            return this.mainPage;
        }
        public void ShowPage(ContentPage page)
        {
            var win = ((Microsoft.Maui.Controls.Window)page.Window);
            var hwnd = ((Microsoft.Maui.MauiWinUIWindow)((Microsoft.Maui.Handlers.ElementHandler)win.Handler).PlatformView).WindowHandle;

            Libs.WinApi.ShowWindow(hwnd, Libs.WinApi.SW_SHOW);
        }
        public void HidePage(ContentPage page)
        {
            var win = ((Microsoft.Maui.Controls.Window)page.Window);
            var hwnd = ((Microsoft.Maui.MauiWinUIWindow)((Microsoft.Maui.Handlers.ElementHandler)win.Handler).PlatformView).WindowHandle;
            
            Libs.WinApi.ShowWindow(hwnd, Libs.WinApi.SW_HIDE);
        }

        public void HidePageShellPage(Shell mainPage)
        {
            var win = ((Microsoft.Maui.Controls.Window)mainPage.Window);
            var hwnd = ((Microsoft.Maui.MauiWinUIWindow)((Microsoft.Maui.Handlers.ElementHandler)win.Handler).PlatformView).WindowHandle;

            Libs.WinApi.ShowWindow(hwnd, Libs.WinApi.SW_HIDE);
        }

        public void SetHomePage(ContentPage mainPage)
        {
            this.mainPage = mainPage;
        }

        public void Show()
        {
            var win = ((Microsoft.Maui.Controls.Window)mainPage.Window);

            var hwnd = ((Microsoft.Maui.MauiWinUIWindow)((Microsoft.Maui.Handlers.ElementHandler)win.Handler).PlatformView).WindowHandle;
            //WinApi.HideCloseButton(hwnd);
            Libs.WinApi.ShowWindow(hwnd, Libs.WinApi.SW_SHOW);
                WinApi.HideCloseButton(hwnd);
        }

        
    }
}
