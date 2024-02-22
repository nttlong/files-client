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

        public ObserveContentService()
        {
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

            //var ret = new Task(() =>
            //{
            //    this.DoWatching(ObervePath);
            //});
            //ret.Start();
            _fileWatcher = new FileSystemWatcher
            {
                Path = ObervePath,

            };

            // Subscribe to events
            //_fileWatcher.Path = ObervePath;
            // _fileWatcher.NotifyFilter = NotifyFilters.c NotifyFilters.LastWrite|NotifyFilters.LastAccess;
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
            try
            {
                if (!(new[] {
                WatcherChangeTypes.Renamed,
                WatcherChangeTypes.Created,
                WatcherChangeTypes.Changed
            }
                ).Contains(e.ChangeType)) { return; }
                //if(e.ChangeType!=WatcherChangeTypes.Renamed) return;
                if ((new FileInfo(e.FullPath)).Name.StartsWith("~")) return;
                var id = e.Name.Split(".").FirstOrDefault();
                var isInCache = this.cacheService.IsContainsRequestInfoById(id);
                if (Utils.DataHashing.IsValidHashKey(id))
                {


                    RequestInfo requestInfo = this.cacheService.GetRequestInfoById(id);
                    if (requestInfo != null)
                    {
                        if (!isInCache)
                        {
                            this.syncContentService.DoUploadContentAsync(
                                requestInfo: requestInfo,
                                BufferSize: this.configService.GetUploadBufferSize(),
                                OnRun: (sizeUploaded, fileSize) =>
                                    {

                                    }).Wait();
                        }
                        else
                        {
                            var checkChangeType = requestInfo.CheckIsChangeAsync().Result;
                            if (checkChangeType != ChangeTypeEnum.None)
                            {
                                this.syncContentService.DoUploadContentAsync(
                                    requestInfo: requestInfo,
                                    BufferSize: this.configService.GetUploadBufferSize(),
                                    OnRun: (sizeUploaded, fileSize) =>
                                    {

                                    }).Wait();
                            }
                        }
                    }
                }
            }
            catch (Models.Exceptions.RequestError ex)
            {
                this.notificationService.ShowError(ex);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
