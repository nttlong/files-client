using CodxClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.ServiceFactory
{
    public class TempFileService : Services.ITempFileService
    {
        private IConfigService configService;
        private INotificationService notificationService;

        public TempFileService() { 
            this.configService=Services.ServiceAssistent.GetService<Services.IConfigService>();
            this.notificationService = ServiceAssistent.GetService<Services.INotificationService>();
        }
        public void Start1() { }
        public void Start()
        {
            var dataPath = this.configService.GetContentDir();
            var trackPath = this.configService.GetContentDir();
            Task.Run(() =>
            {
                while (true)
                {
                    var totalDeleteFile = 0;
                    foreach (string file in Directory.EnumerateFiles(dataPath, "*", SearchOption.AllDirectories))
                    {
                        var fileName = new FileInfo(file).Name;
                        var id = fileName.Split(".").FirstOrDefault();
                        if (Utils.DataHashing.IsValidHashKey(id))
                        {
                            var isFileDelete=false;
                            try
                            {
                                if (File.Exists(file))
                                {
                                    File.Delete(file);
                                    isFileDelete = true;
                                    totalDeleteFile++;
                                }
                            }
                            catch
                            {

                            }
                            if (isFileDelete)
                            {
                                var trackFile = Path.Combine(trackPath, $"{id}.txt");
                                if (File.Exists(trackFile))
                                {
                                    try
                                    {
                                        File.Delete(trackFile);
                                    }
                                    catch
                                    {

                                    }
                                }

                            }

                        }
                        Task.Delay(5).Wait();
                    }
                    if(totalDeleteFile>0)
                    {
                        this.notificationService.ShowNotification("Buffering", $"{totalDeleteFile} files were clear",true);
                    }
                    Task.Delay(30*1000).Wait();
                }
            });
        }
    }
}
