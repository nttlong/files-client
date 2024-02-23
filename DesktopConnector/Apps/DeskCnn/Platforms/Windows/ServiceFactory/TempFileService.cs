using DeskCnn.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskCnn.ServiceFactory
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
                        var isFileDelete = false;
                        FileInfo fileInfo = new FileInfo(file);
                        var fileName = fileInfo.Name;
                        DateTime lastModified = fileInfo.LastWriteTime;
                        var id = fileName.Split(".").FirstOrDefault();
                        if (Utils.DataHashing.IsValidHashKey(id))
                        {
                         
                             
                            // Check if the file was modified more than 2 hours ago
                            if (DateTime.Now.Subtract(lastModified).TotalDays >= 2 )
                            {
                                try
                                {
                                    // Attempt to delete the file
                                    fileInfo.Delete();
                                    isFileDelete = true;
                                    totalDeleteFile++;
                                }
                                catch (Exception ex)
                                {
                                    
                                }
                            }
                            else
                            {
                                Console.WriteLine($"File was last modified less than 2 hours ago: {lastModified.ToString("yyyy-MM-dd HH:mm:ss")}");
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
