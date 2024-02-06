using CodxClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.ServiceFactory
{
    public class ObserveContentService : Services.IObserveContentService
    {
        private Dictionary<string, RequestInfo> Cacher= new Dictionary<string, RequestInfo>();
        private FileSystemWatcher _fileWatcher;

        public void RegisterRequestInfo(RequestInfo info)
        {
            this.Cacher.Add( info.RequestId, info );
        }

        public void Start(string ObervePath)
        {
            _fileWatcher = new FileSystemWatcher
            {
                Path = ObervePath
            };

            // Subscribe to events

            _fileWatcher.Changed += _fileWatcher_Changed;
            _fileWatcher.NotifyFilter = NotifyFilters.LastWrite;

            // Enable watching
            _fileWatcher.EnableRaisingEvents = true;
        }

        private void _fileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
