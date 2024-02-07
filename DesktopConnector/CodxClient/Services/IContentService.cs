using CodxClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Services
{
    public interface IContentService
    {
        Task DownloadAsync(Models.DelelegateInfo src,string SaveToFile);
        Task<RequestInfo> LoadRequestInfoFromFileAsync(string TrackFilePath, string SourceFilePath);
    }
}
