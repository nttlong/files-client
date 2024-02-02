using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxUtils
{
    public class Config
    {
        public string AppDataLocal
        {
            get
            {
                //return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Library", "Application Support");

            }
        }
        static string GetAppDataDir()
        {
            string appDataLocal = Config.();
            string appDirName = "YourAppName"; // Replace with your actual app name
            string dataDirName = "YourDataFolder"; // Replace with your actual data folder name
            string version = "1.0.0"; // Replace with your actual version

            string ret = Path.Combine(appDataLocal, appDirName, dataDirName, version);

            if (!Directory.Exists(ret))
            {
                Directory.CreateDirectory(ret);
            }

            return ret;
        }
    }
}
