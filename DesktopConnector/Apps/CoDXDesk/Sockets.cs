using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace CodxDesk
{
    internal class Sockets
    {
        public static async Task RunAsync()
        {
            var server = new WebSocketServer("ws://localhost:8765");
            server.AddWebSocketService<CodXDeskWebSocketBehavior>("/");
            server.Start();

            await Task.Delay(-1); // Run forever until stopped
        }
    }
}
