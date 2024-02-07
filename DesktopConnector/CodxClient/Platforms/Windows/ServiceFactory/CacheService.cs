using CodxClient.Models;
using CodxClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.ServiceFactory
{
    public class CacheService : ICacheService
    {
        private Dictionary<string, RequestInfo> _cacheRequestInfo;

        public CacheService()
        {
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
    }
}
