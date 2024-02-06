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
        public void Download(Models.DelelegateInfo Src, string SaveToFile )
        {
            Utils.ContentManager.Download(Src, SaveToFile);
        }

        

        public RequestInfo LoadRequestInfoFromFile(string TrackFilePath, string SourceFilePath)
        {
            using (StreamReader reader = File.OpenText(TrackFilePath))
            {

                RequestInfo requestInfo = JsonConvert.DeserializeObject<RequestInfo>(reader.ReadToEnd());
                requestInfo.FilePath = SourceFilePath;
                return requestInfo;
            }
        }
    }
}
