using CodxClient.Models;
using CodxClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.ServiceFactory
{
    public class SyncContentService : ISyncContentService
    {
#if WINDOWS
        [System.Diagnostics.DebuggerStepThrough]
#endif
        public async Task DoUploadContentAsync(RequestInfo requestInfo,long BufferSize, Action<long, long> OnRun)
        {

            requestInfo.Status = RequestInfoStatusEnum.IsUploading;
            await Utils.ContentManager.UploadAsync(requestInfo.Dst, requestInfo.FilePath, BufferSize, OnRun);
            requestInfo.Status = RequestInfoStatusEnum.Ready;
        }

    }
}
