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
using CodxClient.ServiceFactory;


namespace CodxClient.Common
{
    public class CodXDeskWebSocketBehavior : WebSocketBehavior
    {
        private Services.IConfigService config;
        private Services.INotificationService notifyService;
        private Services.IContentService contentService;
        private Services.IOfficeService officeService;
        private IObserveContentService observeContentService;
        private IProcessService processService;

        public CodXDeskWebSocketBehavior()
        {
            
            config = Services.ServiceAssistent.GetService<Services.IConfigService>();
            notifyService = Services.ServiceAssistent.GetService<Services.INotificationService>();
            contentService = Services.ServiceAssistent.GetService<Services.IContentService>();
            officeService = Services.ServiceAssistent.GetService<Services.IOfficeService>();
            observeContentService = Services.ServiceAssistent.GetService<Services.IObserveContentService>();
            processService = Services.ServiceAssistent.GetService<Services.IProcessService>();

        }
        private async Task<Models.RequestInfo?> DoDownloadAsync(Models.RequestInfo Info, string Data)
        {
            var requestId = Utils.DataHashing.HashText(Data);
            this.processService.KillProcessByRequestId(requestId);
            
            Info.TrackFilePath = Path.Combine(this.config.GetTrackDir(), requestId + ".txt");
            Info.FilePath = Path.Combine(this.config.GetContentDir(), requestId)+"."+Info.ResourceExt;
            var IsExisting = false;
            if (System.IO.File.Exists(Info.FilePath))
            {

                try
                {
                    System.IO.File.Delete(Info.FilePath);
                    IsExisting = false;
                }
                catch {
                    IsExisting = true;
                }
                
            }
            if (!IsExisting)
            {
                Info.RequestId = requestId;

                notifyService.ShowNotification("Download", "...");
                await contentService.DownloadAsync(Info.Src, Info.FilePath);
                Info.HashContentList = await Info.GetHashContentOnlineAsync();
                Info.SizeOfFile = Info.GetSizeOnline();
                Info.RequestData = Data;
                await Info.CommitAsync();
                await Info.SaveAsync();
                return Info;
            }
            else
            {
                return null;
            }
            
            
        }
        protected async override void OnMessage(MessageEventArgs e)
        {


            try
            {
                var info = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.RequestInfo>(e.Data);
                bool openOK = false;
                if (DocSupports.AppWordMappingDict.ContainsKey(info.ResourceExt.ToLower()))
                {
                    info = await this.DoDownloadAsync(Info: info, Data: e.Data);

                    openOK = await this.officeService.OpenWordAsync(info);
                    if (openOK)
                    {
                        observeContentService.RegisterRequestInfo(info);
                    }
                }
                else if (DocSupports.ExcelExtensions.ContainsKey(info.ResourceExt.ToLower()))
                {

                    info = await this.DoDownloadAsync(Info: info, Data: e.Data);
                    if (info != null)
                    {
                        observeContentService.RegisterRequestInfo(info);
                        await this.officeService.OpenExcelAsync(info);
                        
                    }
                }
                else if (DocSupports.PowerpointExtensions.ContainsKey(info.ResourceExt.ToLower()))
                {
                    info = await this.DoDownloadAsync(Info: info, Data: e.Data);
                    openOK = await this.officeService.OpenPowerPointAsync(info);
                    if (openOK)
                    {
                        observeContentService.RegisterRequestInfo(info);
                    }
                }
                else if (DocSupports.PaintExtensions.ContainsKey(info.ResourceExt.ToLower()))
                {
                    info = await this.DoDownloadAsync(Info: info, Data: e.Data);
                    openOK = await this.officeService.OpenPaintAsync(info);
                    if (openOK)
                    {
                        observeContentService.RegisterRequestInfo(info);
                    }
                }
                else if (DocSupports.NotepadExtensions.ContainsKey(info.ResourceExt.ToLower()))
                {
                    info = await this.DoDownloadAsync(Info: info, Data: e.Data);
                    openOK = await this.officeService.OpenNotepadAsync(info);
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
            catch (Exception ex)
            {

                notifyService.ShowNotification("error", ex.Message);
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
