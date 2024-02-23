using DeskCnn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskCnn.Services
{
    public interface IContentService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="SaveToFile"></param>
        /// <param name="OnRun"></param>
        /// <exception cref="Models.Exceptions.RequestError"></exception>
        /// <returns></returns>
        Task DownloadAsync(Models.DelelegateInfo src,string SaveToFile, Action<long,long> OnRun);
        Task<RequestInfo> LoadRequestInfoFromFileAsync(string TrackFilePath, string SourceFilePath);
    }
}
