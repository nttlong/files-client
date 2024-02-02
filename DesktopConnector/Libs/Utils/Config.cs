using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Config
    {
        public static string GetAppDataLocal()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Windows
                return Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // macOS
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
            else
            {
                // Assume other OS (Linux, etc.)
                // You might want to handle other operating systems accordingly
                throw new NotSupportedException("Unsupported operating system");
            }
        }
    }
}
