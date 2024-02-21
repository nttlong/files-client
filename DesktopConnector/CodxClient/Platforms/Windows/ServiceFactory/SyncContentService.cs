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
        private IConfigService configService;

        public SyncContentService() { 
            this.notificationService=ServiceAssistent.GetService<INotificationService>();
            this.configService = ServiceAssistent.GetService<IConfigService>();
        }
        

        public async Task DoUploadContentAsync(RequestInfo requestInfo)
        {

            requestInfo.Status = RequestInfoStatusEnum.IsUploading;

            var notifier = this.notificationService.ShowNotificationWithWithProgressBar("File", "...", "File is saving ...", silent: true);
            try
            {
                
                await Utils.ContentManager.UploadAsync(requestInfo.Dst, requestInfo.FilePath,
                    this.configService.GetUploadBufferSize(),
                    (uploadSize,fileSize) => { 
                
                        var rate=(double)((double)uploadSize /fileSize);
                        this.notificationService.UpdateNotifier(notifier, "progressValue", $"{rate}");

                    });
                
            }
            catch (Exception ex)
            {
                requestInfo.CloneToTempFile(this.configService.GetTempDir());
                await Utils.ContentManager.UploadAsync(requestInfo.Dst, requestInfo.FilePath,this.configService.GetUploadBufferSize(), (uploadSize,fileSize) => { 
                
                
                });
                File.Delete(requestInfo.FilePath);
                requestInfo.FilePath = requestInfo.OriginalFilePath;
            }
            //catch (Exception ex)
            //{
            //    this.notificationService.ShowNotification(ex.GetType().FullName, ex.Message, silent: false);
            //}
            await requestInfo.CommitAsync();
            await requestInfo.SaveAsync();
            this.notificationService.UpdateNotifier(notifier, "progressStatus", "File was saved.");
            
            requestInfo.Status = RequestInfoStatusEnum.Ready;


        }
    }
}
