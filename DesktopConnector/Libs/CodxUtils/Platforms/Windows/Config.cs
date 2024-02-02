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
                return Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%");
            }
        }
    }
}
