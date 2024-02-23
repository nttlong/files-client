using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskCnn.Services
{
    public interface IUIService
    {
        void DisableCloseButton(ContentPage mainPage);
        ContentPage? GetHomePage();
        void HidePage(ContentPage mainPage);
        void HidePageShellPage(Shell mainPage);
        void SetHomePage(ContentPage mainPage);
        void Show();
        void ShowPage(ContentPage app);
    }
}
