using CodxClient.Models;
using CodxClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.ServiceFactory
{
    public class RequestManagerService : IRequestManagerService
    {
        private Dictionary<string, RequestInfo> _cacheRequestInfo;

        public RequestManagerService() {
            this._cacheRequestInfo = new Dictionary<string, RequestInfo>();
        }

        

        public void AddRequestInfo(RequestInfo Info)
        {
            if (this._cacheRequestInfo.ContainsKey(Info.RequestId))
            {
                this._cacheRequestInfo.Remove(Info.RequestId);
            }
            this._cacheRequestInfo.Add(Info.RequestId, Info);
        }

        public RequestInfo GetRequestInfoById(string RequestId)
        {
            if (this._cacheRequestInfo.ContainsKey(RequestId))
            {
                return _cacheRequestInfo[RequestId];
            }
            else { return null; }
            
        }

        public bool IsContainsRequestInfoById(string RequestId)
        {
            return _cacheRequestInfo.ContainsKey(RequestId);
        }

        public void RemoveRequestByRequestId(string RequestId)
        {
            if (this._cacheRequestInfo.ContainsKey(RequestId))
            {
                this._cacheRequestInfo.Remove(RequestId);
            }
            
        }
    }
}
