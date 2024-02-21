
using CodxClient.Models;
using CodxClient.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;


namespace CodxClient.ServiceFactory
{
    public class ContentService : Services.IContentService
    {
        private IConfigService configService;

        public ContentService() { 
            this.configService = ServiceAssistent.GetService<IConfigService>();
        }
        public  async  Task DownloadAsync(Models.DelelegateInfo Src,string SaveToFile, Action<long,long> OnRun)
        {
            await Utils.ContentManager.DownloadAsync(Src, SaveToFile,this.configService.GetDownLoadBufferSize(), OnRun);
        }

        public async Task<RequestInfo> LoadRequestInfoFromFileAsync(string TrackFilePath, string SourceFilePath)
        {
            var ret = new Task<RequestInfo>(() =>
            {
                using (StreamReader reader = File.OpenText(TrackFilePath))
                {

                    RequestInfo requestInfo = JsonConvert.DeserializeObject<RequestInfo>(reader.ReadToEnd());
                    requestInfo.FilePath = SourceFilePath;
                    return requestInfo;
                }
            });

            return await ret;
            
        }
    }
}
