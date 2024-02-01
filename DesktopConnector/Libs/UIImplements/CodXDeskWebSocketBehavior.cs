using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using WebSocketSharp;
using UIProviders;
namespace UIImplements
{
    public class CodXDeskWebSocketBehavior : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            ServiceAssistent.GetService<UIProviders.INotificationService>().ShowNotification("Da nhan duoc file", "File da nhan");

            Console.WriteLine(e.Data);
        }
        protected override void OnOpen()
        {
            base.OnOpen();
        }
        protected override void OnError(WebSocketSharp.ErrorEventArgs e)
        {
            base.OnError(e);
        }
        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
        }
    }
}
