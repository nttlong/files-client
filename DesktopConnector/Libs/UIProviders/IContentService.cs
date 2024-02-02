using ConnectorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProviders
{
    public interface IContentService
    {
        void Download(DelelegateInfo src,string SaveToFile);
    }
}
