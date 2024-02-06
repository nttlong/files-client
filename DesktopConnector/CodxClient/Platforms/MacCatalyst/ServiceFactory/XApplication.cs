using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace CodxClient.ServiceFactory
{
    public class XApplication : IApplication
    {
        public IReadOnlyList<IWindow> Windows => throw new NotImplementedException();

        public IElementHandler Handler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IElement Parent => throw new NotImplementedException();

        public void CloseWindow(IWindow window)
        {
            throw new NotImplementedException();
        }

        public IWindow CreateWindow(IActivationState activationState)
        {
            throw new NotImplementedException();
        }

        public void OpenWindow(IWindow window)
        {
            throw new NotImplementedException();
        }

        public void ThemeChanged()
        {
            throw new NotImplementedException();
        }
    }
}
