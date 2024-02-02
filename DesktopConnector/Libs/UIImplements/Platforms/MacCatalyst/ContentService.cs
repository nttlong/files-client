using ConnectorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProviders;

namespace UIImplements
{
    public class ContentService : IContentService
    {
        public void Download(DelelegateInfo Src,string SaveToFile)
        {
            Utils.ContentManager.Download(Src, SaveToFile);
        }
    }
}
