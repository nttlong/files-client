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
            
            try
            {
                requestInfo.Status = RequestInfoStatusEnum.IsUploading;
                await Utils.ContentManager.UploadAsync(requestInfo.Dst, requestInfo.FilePath);
                var notifier = this.notificationService.ShowNotificationWithWithProgressBar("File", "...", "File is saving ...", silent: true);
                this.notificationService.UpdateNotifier(notifier, "progressStatus", "File was saved.");
                this.notificationService.UpdateNotifier(notifier, "progressValue", "1");
                await requestInfo.CommitAsync();
                await requestInfo.SaveAsync();

                requestInfo.Status = RequestInfoStatusEnum.Ready;
            }
            catch (System.IO.IOException e)
            {
                this.notificationService.ShowNotification("Error", e.Message, silent: false);
            }
            catch (Exception e)
            {
                this.notificationService.ShowNotification(e.GetType().FullName, "Error", silent: false);
            }
        }
    }
}
