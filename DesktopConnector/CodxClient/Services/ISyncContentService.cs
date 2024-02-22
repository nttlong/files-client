using CodxClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Services
{
    public interface ISyncContentService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="BufferSize"></param>
        /// <param name="OnRun"></param>
        /// <exception cref="CodxClient.Models.Exceptions.RequestError"></exception>
        /// <returns></returns>
        public Task DoUploadContentAsync(RequestInfo requestInfo,long BufferSize,Action<long,long> OnRun);
    }
}
