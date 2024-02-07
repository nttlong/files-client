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
            bool isChange = await requestInfo.CheckIsChangeAsync();
            if (isChange)
            {
                try
                {
                    requestInfo.Status = RequestInfoStatusEnum.IsUploading;
                    await Utils.ContentManager.UploadAsync(requestInfo.Dst, requestInfo.FilePath);
                    this.notificationService.ShowNotification("File", "Uploaded");
                    requestInfo.Status = RequestInfoStatusEnum.Unknown;
                    await requestInfo.CommitAsync();
                    await requestInfo.SaveAsync();
                }
                catch(System.IO.IOException)
                {
                    this.notificationService.ShowNotification("File", "is saving ...");
                }
                catch (Exception e)
                {
                    this.notificationService.ShowNotification(e.GetType().FullName, "Error");
                }
                finally
                {
                    requestInfo.Status = RequestInfoStatusEnum.Unknown;
                }
            }
        }
    }
}
