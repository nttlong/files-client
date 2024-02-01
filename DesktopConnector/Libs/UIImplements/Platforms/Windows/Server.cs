using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UIProviders;
using WebSocketSharp.Server;

namespace UIImplements
{
    public class Server : IServer
    {
        public Task RunAsync()
        {
            var ret = new Task(() =>
            {
                var server = new WebSocketServer("ws://127.0.0.1:8765");
                server.AddWebSocketService<CodXDeskWebSocketBehavior>("/");
                server.Start();
            });

            return ret;
        }

    }
}
