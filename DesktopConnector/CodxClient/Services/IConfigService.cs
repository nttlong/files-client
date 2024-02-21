using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Services
{
    public interface IConfigService
    {
        void initialize();
        string GetAppDataLocal();
        string GetAppDataDir();
        string GetVersion();
        string GetAppName();
        /// <summary>
        /// All tracking info here
        /// </summary>
        /// <returns></returns>
        string GetTrackDir();
        /// <summary>
        /// All content file buffering here
        /// </summary>
        /// <returns></returns>
        string GetContentDir();
        void SetAutoStartUp(bool IsAuto);
    }
}
