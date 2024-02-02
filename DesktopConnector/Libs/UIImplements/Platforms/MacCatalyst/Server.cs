﻿using System;
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
        static WebSocketServer server = null;
        public Task RunAsync(string url)
        {
            var ret = new Task(() =>
            {
                if (server == null)
                {
                    server = new WebSocketServer(url);
                    server.AddWebSocketService<CodXDeskWebSocketBehavior>("/");
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
