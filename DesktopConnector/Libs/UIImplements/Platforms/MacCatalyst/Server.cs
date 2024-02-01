using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UIProviders;
using WebSocketSharp;
using WebSocketSharp.Server;
using WebSocketSharp;
namespace UIImplements
{
    public class Server : IServer
    {
        public async Task RunAsync()
        {
            var server = new WebSocketServer("ws://localhost:8765");
            server.AddWebSocketService<CodXDeskWebSocketBehavior>("/");
            server.Start();

            await Task.Delay(-1); // Run forever until stopped
        }
    }
}
