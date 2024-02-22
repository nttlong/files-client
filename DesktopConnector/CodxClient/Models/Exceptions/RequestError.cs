using System.Net;
using System.Runtime.Serialization;

namespace CodxClient.Models.Exceptions
{
    [Serializable]
    public class RequestError : Exception
    {
        public string Url { get; }

        public HttpStatusCode Code { get; }

        public RequestError()
        {
        }

        public RequestError(string message) : base(message)
        {
        }

        public RequestError(string Message, string Url, HttpStatusCode Code) : base(Message)
        {
            this.Url = Url;
            this.Code = Code;
        }

        public RequestError(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequestError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}