using CodxClient.Models;
using CodxClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.ServiceFactory
{
    public class SyncContentService : ISyncContentService
    {
        private INotificationService notificationService;

        public SyncContentService() { 
            this.notificationService=ServiceAssistent.GetService<INotificationService>(); 
        }
        

        public async Task DoUploadContentAsync(RequestInfo requestInfo)
        {

            if (requestInfo.Status != RequestInfoStatusEnum.IsUploading)
            {
                try
                {
                    requestInfo.Status = RequestInfoStatusEnum.IsUploading;
                    await Utils.ContentManager.UploadAsync(requestInfo.Dst, requestInfo.FilePath);
                    this.notificationService.ShowNotification("File", "Uploaded");
                    requestInfo.Status = RequestInfoStatusEnum.Unknown;
                }
                catch(Exception e)
                {
                    this.notificationService.ShowNotification("Error", "Uploaded");
                }
                finally
                {
                    requestInfo.Status = RequestInfoStatusEnum.Unknown;
                }
            }
        }
    }
}
