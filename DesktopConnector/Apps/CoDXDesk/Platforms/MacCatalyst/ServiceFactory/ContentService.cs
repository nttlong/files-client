
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoDXDesk.ServiceFactory
{
    public class ContentService : Services.IContentService
    {
        public void Download(Models.DelelegateInfo Src,string SaveToFile)
        {
            Utils.ContentManager.Download(Src, SaveToFile);
        }
    }
}
