using CodxClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodxClient.ServiceFactory
{
    public class ContentService : Services.IContentService
    {
        [System.Diagnostics.DebuggerStepThrough]
        public async Task DownloadAsync(Models.DelelegateInfo Src, string SaveToFile )
        {
           await Utils.ContentManager.DownloadAsync(Src, SaveToFile);
        }

        

        public async Task<RequestInfo> LoadRequestInfoFromFileAsync(string TrackFilePath, string SourceFilePath)
        {
            var syncFunctionResult = await Task.Run(() =>
            {
                using (StreamReader reader = File.OpenText(TrackFilePath))
                {

                    RequestInfo requestInfo = JsonConvert.DeserializeObject<RequestInfo>(reader.ReadToEnd());
                    requestInfo.FilePath = SourceFilePath;
                    requestInfo.RequestId = System.IO.Path.GetFileNameWithoutExtension(TrackFilePath);
                    return requestInfo;
                }
            });
            return syncFunctionResult;
            
        }
    }
}
