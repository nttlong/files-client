using CodxClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Services
{
    /// <summary>
    /// This service use for all pull out and push up request of file tracking
    /// </summary>
    public interface IRequestManagerService
    {
        /// <summary>
        /// Add new request
        /// </summary>
        /// <param name="info"></param>
        void AddRequestInfo(RequestInfo info);
        RequestInfo GetRequestInfoById(string RequestId);
        bool IsContainsRequestInfoById(string RequestId);
        void RemoveRequestByRequestId(string RequestId);
    }
}
