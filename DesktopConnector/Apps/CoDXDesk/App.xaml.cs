using UIProviders;
using UIImplements;
namespace CoDXDesk
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            
        }
        
    }
}
