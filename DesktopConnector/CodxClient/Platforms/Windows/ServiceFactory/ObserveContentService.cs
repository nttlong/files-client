using CodxClient.Models;
using CodxClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.ServiceFactory
{
    public class ObserveContentService : Services.IObserveContentService
    {
        private FileSystemWatcher _fileWatcher;
        private Dictionary<string,RequestInfo> Cacher=new Dictionary<string, RequestInfo>();
        private INotificationService notificationService;
        private IContentService contentService;
        private IConfigService configService;

        public ObserveContentService() {
            this.notificationService = ServiceAssistent.GetService<Services.INotificationService>();
            this.contentService = ServiceAssistent.GetService<Services.IContentService>();
            this.configService = ServiceAssistent.GetService<Services.IConfigService>();
        }
        public void RegisterRequestInfo(RequestInfo info)
        {
            this.Cacher.Add(info.RequestId, info);
        }
        public void Start(string ObervePath)
        {
            _fileWatcher = new FileSystemWatcher
            {
                Path = ObervePath,
                
            };

            // Subscribe to events
            //_fileWatcher.Path = ObervePath;
            //_fileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;
            _fileWatcher.IncludeSubdirectories = false; // To monitor subfolders as well
            _fileWatcher.EnableRaisingEvents = true; // Start watching
            _fileWatcher.Changed += _fileWatcher_Changed;
            _fileWatcher.Created += _fileWatcher_Changed;
            _fileWatcher.Deleted += _fileWatcher_Changed;
            _fileWatcher.Renamed += _fileWatcher_Changed;

            //_fileWatcher.NotifyFilter = NotifyFilters.LastWrite;

            // Enable watching
            _fileWatcher.EnableRaisingEvents = true;
        }

        private void _fileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            var id = e.Name.Split(".").FirstOrDefault();
            if (Utils.DataHashing.IsValidHashKey(id))
            {
                if (this.Cacher.ContainsKey(id))
                {
                    if (e.ChangeType != WatcherChangeTypes.Deleted)
                    {
                        RequestInfo requestInfo = this.Cacher[id]; 
                        this.notificationService.ShowNotification("File", "Checking ...");
                    }
                }
                else
                {
                    if (e.ChangeType != WatcherChangeTypes.Deleted)
                    {
                        var trackFilePath = Path.Combine(this.configService.GetTrackDir(), id + ".txt");
                        if (File.Exists(trackFilePath))
                        {
                            RequestInfo requestInfo = this.contentService.LoadRequestInfoFromFile(trackFilePath: trackFilePath);
                        }
                        this.notificationService.ShowNotification("File", "Checking ...");
                    }
                }
            }
            //throw new NotImplementedException();
        }
    }
}
