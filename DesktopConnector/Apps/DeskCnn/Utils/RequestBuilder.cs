
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskCnn.Utils
{
    public class RequestBuilder
    {
        public static Models.Info CreateRequest(string data)
        {
            Models.RequestInfo request = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.RequestInfo>(data);
            request.HashContent = DataHashing.HashText(data);
            throw new NotImplementedException();
        }
    }
}
