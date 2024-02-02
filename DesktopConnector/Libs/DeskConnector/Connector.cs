using ConnectorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskConnector
{
    public class Connector
    {
        public static void DoConnect(string data)
        {
            var id = CodxUtils.DataHashing.HashText(data);
            Info request = CodxUtils.RequestBuilder.CreateRequest(data);
            throw new NotImplementedException();
        }
    }
}
