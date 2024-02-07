using CodxClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Services
{
    public interface ICacheService
    {
        void AddRequestInfo(RequestInfo info);
        RequestInfo GetRequestInfoById(string RequestId);
        bool IsContainsRequestInfoById(string RequestId);
    }
}
