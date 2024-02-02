using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProviders
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
