using DeskCnn.Models;
using DeskCnn.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeskCnn.ServiceFactory
{
    public class ContentService : Services.IContentService
    {
        private IConfigService configService;

        public ContentService() { 
            this.configService= ServiceAssistent.GetService<IConfigService>();
        }
        [System.Diagnostics.DebuggerStepThrough]
        public async Task DownloadAsync(Models.DelelegateInfo Src, string SaveToFile,Action<long,long> OnRun )
        {
           await Utils.ContentManager.DownloadAsync(Src, SaveToFile, configService.GetDownLoadBufferSize(), OnRun);
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
