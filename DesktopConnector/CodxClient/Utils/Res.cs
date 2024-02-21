using CodxClient.Properties.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Utils
{
    public class Res
    {
        private static ResourceManager _resourceManager;

        public static string Get(string Key)
        {
            
            var ret= AppResources.ResourceManager.GetString(Key,AppResources.Culture);
            if(ret == null)
            {
                return Key;
            }
            else
            {
                return ret;
            }
        }
    }
}
