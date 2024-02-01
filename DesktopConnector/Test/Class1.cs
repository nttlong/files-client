using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Test
{
    

    public class YourWebSocketBehavior : WebSocketBehavior
    {
        protected override async void OnMessage(MessageEventArgs e)
        {
            try
            {
                var data = JsonConvert.DeserializeObject(e.Data);
                var checkData = YourController.CheckData(data);

                if (checkData.ContainsKey("error_code"))
                {
                    YourUIController.Loader.ShowMessageError(checkData["error_message"]);
                    await YourController.SendErrorToClientAsync(Context.WebSocket, "system", "System error");
                }

                var result = await YourController.ResolveAsync(Context.WebSocket, checkData);

                if (result == null)
                {
                    await Send("error");
                    return;
                }

                var id = result.Id;
            }
            catch (YourError e)
            {
                YourUIController.Loader.ShowMessageError(e.Message);
                await YourController.SendErrorToClientAsync(Context.WebSocket, "system", "System error");
            }
            catch (Exception e)
            {
                throw e;
                // YourUIController.Loader.ShowMessageError(e.Message);
                // await YourController.SendErrorToClientAsync(Context.WebSocket, "system", "System error");
            }
        }
    }

    public class YourWebSocketServer
    {
        public static async Task RunAsync()
        {
            using (var server = new WebSocketServer("ws://localhost:8765"))
            {
                server.AddWebSocketService<YourWebSocketBehavior>("/");
                server.Start();

                await Task.Delay(-1); // Run forever until stopped
            }
        }
    }

}
