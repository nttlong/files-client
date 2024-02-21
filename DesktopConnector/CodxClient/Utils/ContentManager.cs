
using CodxClient.Models;
using Microsoft.Toolkit.Uwp.Notifications;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Reflection;
using System.Text;
//using Windows.Media.Protection.PlayReady;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodxClient.Utils
{
    public class ProgressStreamContent : StreamContent
    {
        private readonly HttpContent originalContent;
        private Stream streamToWrite;

        public int ChunkSize { get; }
        public Action<long> OnReadStream { get; }

        public ProgressStreamContent(Stream streamToWrite,int ChunkSize,Action <long> OnReadStream) : base(streamToWrite)
        {
            this.streamToWrite = streamToWrite;
            this.ChunkSize = ChunkSize;
            this.OnReadStream = OnReadStream;
        }
        protected override bool TryComputeLength(out long length)
        {
            
            length = this.streamToWrite.Length;
            return true;
        }
        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            var buffer = new byte[this.ChunkSize];
            var size = this.streamToWrite.Length;
            var uploaded = 0;

            using (this.streamToWrite)
            {
                while (true)
                {
                    var length = this.streamToWrite.Read(buffer, 0, buffer.Length);
                    if (length <= 0)
                    {
                        break;
                    }

                    uploaded += length;
                    this.OnReadStream(uploaded);
                    await stream.WriteAsync(buffer, 0, length);
                }
            }
        }
    }
    public class ContentManager
    {
//#if WINDOWS
//        [System.Diagnostics.DebuggerStepThrough]
//#endif
        public static async Task<HttpResponseMessage> CreateRequestAsync(string url, string method, Dictionary<string, string> header, object data, string filepathToDownload, string filePathtoUpload,long BufferSize,Action<long,long> OnRun)
        {
            HttpClient httpClient = new HttpClient();
            HttpMethod httpMethod = new HttpMethod(method);
            HttpRequestMessage request = new HttpRequestMessage(httpMethod, url);

            // Add headers
            //request.Headers.Add("mac_address_id", $"{Guid.NewGuid():N}");
            //request.Headers.Add("hash_contents", hashToServer);
            //request.Headers.Add("hash_len", hashLen);

            if (data != null && filePathtoUpload == null)
            {
                //request.Headers.Add("Content-Type", "application/json");
                request.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            }

            if (header != null)
            {
                foreach (var entry in header)
                {
                    request.Headers.Add(entry.Key, entry.Value);
                }
            }

            if (filePathtoUpload != null)
            {
                var mode = new FileStreamOptions();
                mode.Options = FileOptions.Asynchronous;
                mode.Access = FileAccess.Read;
                mode.BufferSize =(int) BufferSize;
                mode.Share = FileShare.Read;

                //FileStream fileStream = new FileStream(filePathtoUpload, mode);

                using (var stream =  new FileStream(filePathtoUpload, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                
                    //using (var stream = new FileStream(filePathtoUpload, FileMode.Open,FileAccess.Read,FileShare.Read,1024,false))
                using (var uploadStream= new ProgressStreamContent(stream,(int)BufferSize, (uploaded) =>
                {
                    OnRun(uploaded, stream.Length);
                }))
                {
                    
                    var fileName = Path.GetFileName(filePathtoUpload);
                    if (data != null)
                    {
                        var dataContent = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                        var postDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(dataContent);
                        MultipartFormDataContent formDataContent = new MultipartFormDataContent();
                        
                        formDataContent.Add(uploadStream, "content", fileName);

                        foreach (KeyValuePair<string, string> entry in postDict)
                        {
                            formDataContent.Add(new StringContent(entry.Value), entry.Key);
                        }
                        request.Content = formDataContent;
                    }
                    else
                    {
                        MultipartFormDataContent formDataContent = new MultipartFormDataContent();

                        formDataContent.Add(uploadStream, "content", fileName);
                        request.Content = formDataContent;
                    }
                    
                    var ret = await httpClient.SendAsync(request);
                    
                    return ret;
                }
                
            }
            else if (filepathToDownload != null)
            {
                var ret = await httpClient.SendAsync(request);
                long bytesSent = 0;
                byte[] buffer = new byte[BufferSize];
                int bytesRead;
                using (var fileStream = new FileStream(filepathToDownload, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, (int)BufferSize, true))
                {
                    while ((bytesRead = ret.Content.ReadAsStream().Read(buffer, 0, buffer.Length)) > 0)
                    {
                        // Send the buffer to the server (assuming you have logic for that)
                        fileStream.Write(buffer, 0, bytesRead);
                        bytesSent += bytesRead;
                        OnRun(bytesSent, ret.Content.Headers.ContentLength??0);
                    }
                }
                return ret;
            }
            else { return null; }


        }
#if WINDOWS
        [System.Diagnostics.DebuggerStepThrough]
#endif
        public async static Task DownloadAsync(Models.DelelegateInfo Src, string SaveToFile,long BufferSize,Action<long,long> OnRun)
        {
            var response = await CreateRequestAsync(Src.Url, Src.Method, Src.Header, Src.data, SaveToFile, null, BufferSize,OnRun);
            

        }
//#if WINDOWS
//        [System.Diagnostics.DebuggerStepThrough]
//#endif
        public async static Task UploadAsync(DelelegateInfo Dst, string UploadFile, long BufferSize, Action<long,long> OnRun)
        {
            await CreateRequestAsync(Dst.Url, Dst.Method, Dst.Header, Dst.data, null, UploadFile, BufferSize, OnRun);

        }
    }
}