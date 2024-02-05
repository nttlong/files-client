
namespace CoDXDesk
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            
            //this.IsVisible = false;
        }

        
        protected override void OnAppearing()
        {
            var fx = this.Window;
            //base.OnAppearing();
#if WINDOWS
            var win = ((Microsoft.Maui.Controls.Window)this.Window);
            var hwnd = ((Microsoft.Maui.MauiWinUIWindow)((Microsoft.Maui.Handlers.ElementHandler)win.Handler).PlatformView).WindowHandle;
            WinApi.ShowWindow(hwnd,WinApi.SW_HIDE);
#endif
            //((Microsoft.Maui.MauiWinUIWindow)del).WindowHandle
            //this.IsVisible = false;
        }
        protected override void ChangeVisualState()
        {
            //var fx = this.Window;
            //base.ChangeVisualState();
#if WINDOWS
            var win = ((Microsoft.Maui.Controls.Window)this.Window);
            var hwnd = ((Microsoft.Maui.MauiWinUIWindow)((Microsoft.Maui.Handlers.ElementHandler)win.Handler).PlatformView).WindowHandle;
            WinApi.ShowWindow(hwnd,WinApi.SW_HIDE);
#endif
        }
        private void OnClosed(object sender, EventArgs e)
        {

        }
        private void OnClosing(object sender, EventArgs e)
        {

        }
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            //SemanticScreenReader.Announce(CounterBtn.Text);
        }
        private void OnCounterClicked2(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn2.Text = $"Clicked {count} time";
            else
                CounterBtn2.Text = $"Clicked {count} times";

            //SemanticScreenReader.Announce(CounterBtn.Text);
        }

    }

}
