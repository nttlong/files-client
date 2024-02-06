using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using WebSocketSharp;
//using NetOffice.OfficeApi.Tools.Informations;

using System.IO;
using CodxClient.Services;


namespace CodxClient.Common
{
    public class CodXDeskWebSocketBehavior : WebSocketBehavior
    {
        private Services.IConfigService config;
        private Services.INotificationService notifyService;
        private Services.IContentService contentService;
        private Services.IOfficeService officeService;
        private IObserveContentService observeContentService;

        public CodXDeskWebSocketBehavior()
        {
            
            config = Services.ServiceAssistent.GetService<Services.IConfigService>();
            notifyService = Services.ServiceAssistent.GetService<Services.INotificationService>();
            contentService = Services.ServiceAssistent.GetService<Services.IContentService>();
            officeService = Services.ServiceAssistent.GetService<Services.IOfficeService>();
            observeContentService = Services.ServiceAssistent.GetService<Services.IObserveContentService>();

        }
        private Models.RequestInfo DoDownload(Models.RequestInfo Info, string Data)
        {
            var requestId = Utils.DataHashing.HashText(Data);
            Info.TrackFilePath = Path.Combine(this.config.GetTrackDir(), requestId + ".txt");
            Info.FilePath = Path.Combine(this.config.GetContentDir(), requestId)+"."+Info.ResourceExt;
            if (System.IO.File.Exists(Info.FilePath))
            {
                System.IO.File.Delete(Info.FilePath);
            }
            Info.RequestId = requestId;
            File.WriteAllText(Info.TrackFilePath, Data);
            notifyService.ShowNotification("Download", "...");
            contentService.Download(Info.Src, Info.FilePath);
            return Info;
        }
        protected override void OnMessage(MessageEventArgs e)
        {


            var info = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.RequestInfo>(e.Data);
            bool openOK = false;
            if (DocSupports.AppWordMappingDict.ContainsKey(info.ResourceExt.ToLower()))
            {
                info = this.DoDownload(Info: info, Data: e.Data);

                openOK=this.officeService.OpenWord(info.FilePath);
                if(openOK)
                {
                    observeContentService.RegisterRequestInfo(info);
                }
            }
            else if (DocSupports.ExcelExtensions.ContainsKey(info.ResourceExt.ToLower()))
            {
                info = this.DoDownload(Info: info, Data: e.Data);
                openOK = this.officeService.OpenExcel(info.FilePath);
                if (openOK)
                {
                    observeContentService.RegisterRequestInfo(info);
                }
            }
            else if (DocSupports.PowerpointExtensions.ContainsKey(info.ResourceExt.ToLower()))
            {
                info = this.DoDownload(Info: info, Data: e.Data);
                openOK = this.officeService.OpenPowerPoint(info.FilePath);
                if (openOK)
                {
                    observeContentService.RegisterRequestInfo(info);
                }
            }
            else if (DocSupports.PaintExtensions.ContainsKey(info.ResourceExt.ToLower()))
            {
                info = this.DoDownload(Info: info, Data: e.Data);
                openOK = this.officeService.OpenPaint(info.FilePath);
                if (openOK)
                {
                    observeContentService.RegisterRequestInfo(info);
                }
            }
            else if (DocSupports.NotepadExtensions.ContainsKey(info.ResourceExt.ToLower()))
            {
                info = this.DoDownload(Info: info, Data: e.Data);
                openOK = this.officeService.OpenNotepad(info.FilePath);
                if (openOK)
                {
                    observeContentService.RegisterRequestInfo(info);
                }
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
