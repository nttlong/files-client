using ConnectorModel;
using System.Text.Json;
namespace CodxUtils
{
    public class RequestBuilder
    {
        public static Info CreateRequest(string data)
        {
            ConnectorModel.RequestInfo request  = Newtonsoft.Json.JsonConvert.DeserializeObject<ConnectorModel.RequestInfo>(data);
            request.HashContent = DataHashing.HashText(data);
            throw new NotImplementedException();
        }
    }
}