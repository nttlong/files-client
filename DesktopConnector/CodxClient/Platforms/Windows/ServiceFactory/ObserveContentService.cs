using CodxClient.Models;
using CodxClient.Services;
using Microsoft.UI.Xaml.Markup;
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
        private IRequestManagerService cacheService = null;
        private INotificationService notificationService;
        private IContentService contentService;
        private IConfigService configService;
        private ISyncContentService syncContentService;

        public ObserveContentService() {
            this.notificationService = ServiceAssistent.GetService<Services.INotificationService>();
            this.contentService = ServiceAssistent.GetService<Services.IContentService>();
            this.configService = ServiceAssistent.GetService<Services.IConfigService>();
            this.syncContentService = ServiceAssistent.GetService<Services.ISyncContentService>();
            this.cacheService = ServiceAssistent.GetService<IRequestManagerService>();
        }
        public void RegisterRequestInfo(RequestInfo info)
        {
            this.cacheService.AddRequestInfo(info);
            
        }
        public void Start1(string ObervePath)
        {
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
        private async Task HandleFileChangeAsync(object sender, FileSystemEventArgs e)
        {
            
        }
        private void _fileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!(new[] { WatcherChangeTypes.Renamed,WatcherChangeTypes.Created }).Contains(e.ChangeType)) { return; }   
            //if(e.ChangeType!=WatcherChangeTypes.Renamed) return;
            if ((new FileInfo(e.FullPath)).Name.StartsWith("~")) return;
            var id = e.Name.Split(".").FirstOrDefault();
            if (Utils.DataHashing.IsValidHashKey(id))
            {
                if (this.cacheService.IsContainsRequestInfoById(id))
                {
                    RequestInfo requestInfo = this.cacheService.GetRequestInfoById(id);
                    var checkChangeType = requestInfo.CheckIsChangeAsync().Result;
                    if (checkChangeType!=ChangeTypeEnum.None)
                    {
                        this.syncContentService.DoUploadContentAsync(requestInfo).Wait();
                    }
                }
                //else
                //{
                //    if (e.ChangeType != WatcherChangeTypes.Deleted)
                //    {
                //        var trackFilePath = Path.Combine(this.configService.GetTrackDir(), id + ".txt");
                //        if (File.Exists(trackFilePath))
                //        {
                //            Task.Run(async () => {

                //                RequestInfo requestInfo = await this.contentService.LoadRequestInfoFromFileAsync(TrackFilePath: trackFilePath, SourceFilePath: e.FullPath);
                //                await this.syncContentService.DoUploadContentAsync(requestInfo);
                //            });
                //        }

                //    }
                //}
            }
        }
    }
}
