
using System.IO;
using System.Reflection;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoDXDesk.Utils
{
    public class ContentManager
    {

        static HttpResponseMessage CreateRequest(string url, string method, Dictionary<string, string> header, object data,string filepathToDownload, string filePathtoUpload)
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
                using (var stream = new FileStream(filePathtoUpload, FileMode.Open, FileAccess.Read))
                {
                    var fileContent = new StreamContent(stream);
                    var fileName = Path.GetFileName(filePathtoUpload);

                    request.Content = new MultipartFormDataContent
                {
                    { fileContent, "content", fileName }
                };
                }
            }
            
            var ret= httpClient.SendAsync(request).Result;
            if(filepathToDownload!=null)
            {
                using (Stream contentStream =  ret.Content.ReadAsStreamAsync().Result, fileStream = new FileStream(filepathToDownload, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                {
                    contentStream.CopyToAsync(fileStream).Wait();
                }
            }
            
            return ret;
        }
        internal static void Download(Models.DelelegateInfo Src, string SaveToFile)
        {
            CreateRequest(Src.Url, Src.Method, Src.Header, Src.data, SaveToFile, null);
        }

        
    }
}