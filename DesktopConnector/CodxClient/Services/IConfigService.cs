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
        string GetTrackDir();
        string GetContentDir();
    }
}
