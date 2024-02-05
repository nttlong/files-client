namespace CodxClient
{
    public class TrayIconPage : ContentPage
    {
        public TrayIconPage()
        {
            Content = new Grid
            {
                Children =
                {
                    new Label
                    {
                        Text = "Welcome to .NET MAUI!!!",
                        TextColor = Colors.Purple,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    }
                }
            };
        }
    }
}
