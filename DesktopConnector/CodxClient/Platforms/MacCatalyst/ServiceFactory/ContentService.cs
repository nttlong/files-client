
using CodxClient.Models;
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
        public  async  Task DownloadAsync(Models.DelelegateInfo Src,string SaveToFile)
        {
            await Utils.ContentManager.DownloadAsync(Src, SaveToFile);
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
