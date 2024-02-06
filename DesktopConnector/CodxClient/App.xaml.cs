using CodxClient.Services;

namespace CodxClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            this.MainPage = new AppShell();
        }
        protected override void OnStart()
        {
            base.OnStart();
            //var hwnd = ((Microsoft.Maui.MauiWinUIWindow)((Microsoft.Maui.Handlers.ElementHandler<Microsoft.Maui.IWindow, Microsoft.UI.Xaml.Window>)((Microsoft.Maui.Controls.Element)((Microsoft.Maui.Handlers.ElementHandler<Microsoft.Maui.IWindow, Microsoft.UI.Xaml.Window>)this.MainPage.Window.Handler).VirtualView).Handler).PlatformView).WindowHandle;
            ServiceAssistent.GetService<IUIService>().HidePageShellPage((Shell)this.MainPage);
        }
        public override void CloseWindow(Window window)
        {
            base.CloseWindow(window);
        }
    }
}
