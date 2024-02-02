using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using WebSocketSharp;
using UIProviders;
using ConnectorModel;

namespace UIImplements
{
    public class CodXDeskWebSocketBehavior : WebSocketBehavior
    {
        private IConfigService config;
        private INotificationService notifyService;
        private IContentService contentService;
        private IWordService wordService;

        public CodXDeskWebSocketBehavior() {
            config = ServiceAssistent.GetService<IConfigService>();
            notifyService = ServiceAssistent.GetService<INotificationService>();
            contentService = ServiceAssistent.GetService<IContentService>();
            wordService = ServiceAssistent.GetService<IWordService>();
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            var requestId = Utils.DataHashing.HashText(e.Data);
            
            var info = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestInfo>(e.Data);
            if (DocSupports.AppWordMappingDict.ContainsKey(info.ResourceExt))
            {
                info.TrackFilePath = Path.Combine(this.config.GetTrackDir(), requestId + ".txt");
                info.FilePath = Path.Combine(this.config.GetContentDir(), requestId);

                File.WriteAllText(info.TrackFilePath, e.Data);
                notifyService.ShowNotification("Dowload", "...");
                contentService.Download(info.Src, info.FilePath);
                this.wordService.OpenFile(info.FilePath);
            }
            else
            {
                notifyService.ShowNotification("Error", "File type is not support");
            }



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
