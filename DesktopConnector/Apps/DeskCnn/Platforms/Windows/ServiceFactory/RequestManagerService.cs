using DeskCnn.Models;
using DeskCnn.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskCnn.ServiceFactory
{
    public class RequestManagerService : IRequestManagerService
    {
        private ConcurrentDictionary<string, RequestInfo> _cacheRequestInfo;
        private IConfigService configService;

        public RequestManagerService()
        {
            this._cacheRequestInfo = new ConcurrentDictionary<string, RequestInfo>();
            this.configService = ServiceAssistent.GetService<IConfigService>();
        }



        public void AddRequestInfo(RequestInfo Info)
        {
            if (this._cacheRequestInfo.ContainsKey(Info.RequestId))
            {
                var req=new RequestInfo();
                this._cacheRequestInfo.Remove(Info.RequestId,out req);
                req = null;
                GC.Collect();
            }
            this._cacheRequestInfo.TryAdd(Info.RequestId, Info);
        }

        public RequestInfo GetRequestInfoById(string RequestId)
        {
            if (this._cacheRequestInfo.ContainsKey(RequestId))
            {
                return _cacheRequestInfo[RequestId];
            }
            else {
                var infoPath = Path.Combine(this.configService.GetTrackDir(), $"{RequestId}.txt");
                if(File.Exists(infoPath))
                {
                    string jsonString = File.ReadAllText(infoPath);
                    var ret = JsonConvert.DeserializeObject<RequestInfo>(jsonString);
                    this._cacheRequestInfo.TryAdd(RequestId,ret);
                    return ret;
                }
                return null;
            }

        }

        public bool IsContainsRequestInfoById(string RequestId)
        {
            return _cacheRequestInfo.ContainsKey(RequestId);
        }

        public void RemoveRequestByRequestId(string RequestId)
        {
            if (this._cacheRequestInfo.ContainsKey(RequestId))
            {
                var req=new RequestInfo();
                this._cacheRequestInfo.Remove(RequestId, out req);
                req = null;
                GC.Collect();
            }

        }
    }
}
