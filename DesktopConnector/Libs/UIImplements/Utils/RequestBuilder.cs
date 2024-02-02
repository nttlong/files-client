using ConnectorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIImplements.Utils
{
    public class RequestBuilder
    {
        public static Info CreateRequest(string data)
        {
            ConnectorModel.RequestInfo request = Newtonsoft.Json.JsonConvert.DeserializeObject<ConnectorModel.RequestInfo>(data);
            request.HashContent = DataHashing.HashText(data);
            throw new NotImplementedException();
        }
    }
}
