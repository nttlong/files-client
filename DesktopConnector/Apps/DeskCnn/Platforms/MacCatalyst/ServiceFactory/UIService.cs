using DeskCnn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskCnn.ServiceFactory
{
    public class UIService : IUIService
    {
        ContentPage? mainPage = null;

        public void DisableCloseButton(ContentPage mainPage)
        {
            throw new NotImplementedException();
        }

        public ContentPage? GetHomePage()
        {
            return mainPage;
        }

        public void HidePage(ContentPage mainPage)
        {
            throw new NotImplementedException();
        }

        public void HidePageShellPage(Shell mainPage)
        {
            throw new NotImplementedException();
        }

        public void SetHomePage(ContentPage mainPage)
        {
            this.mainPage = mainPage;
        }

        public void Show()
        {
            throw new NotImplementedException();
        }

        public void ShowPage(ContentPage app)
        {
            throw new NotImplementedException();
        }
    }
}
