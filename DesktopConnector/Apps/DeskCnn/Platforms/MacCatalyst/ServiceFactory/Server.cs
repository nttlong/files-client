using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using WebSocketSharp;
using WebSocketSharp.Server;
using System.Threading;

namespace DeskCnn.ServiceFactory
{
    public class Server : Services.IServer
    {
        static WebSocketServer server = null;
        public Task RunAsync(string url)
        {
            var ret = new Task(() =>
            {
                if (server == null)
                {
                    server = new WebSocketServer(url);
                    server.AddWebSocketService<Common.CodXDeskWebSocketBehavior>("/");
                    server.Start();
                }
            });
            Task.Delay(500);
            new Task(() =>
            {
                using (var client = new ClientWebSocket())
                {
                    // Replace with the actual WebSocket server URL

                    client.ConnectAsync(new Uri(url), CancellationToken.None).Wait();
                }
            }).Start();

            return ret;
        }
    }
}
