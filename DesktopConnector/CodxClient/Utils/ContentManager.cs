
using CodxClient.Models;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodxClient.Utils
{
    public class ContentManager
    {
//#if WINDOWS
//        [System.Diagnostics.DebuggerStepThrough]
//#endif
        public static async Task<HttpResponseMessage> CreateRequestAsync(string url, string method, Dictionary<string, string> header, object data, string filepathToDownload, string filePathtoUpload)
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
                using (var stream = new FileStream(filePathtoUpload, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var fileContent = new StreamContent(stream))
                    {
                        var fileName = Path.GetFileName(filePathtoUpload);
                        if (data != null)
                        {
                            var dataContent = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                            var postDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(dataContent);
                            MultipartFormDataContent formDataContent = new MultipartFormDataContent();
                            formDataContent.Add(fileContent, "content", fileName);

                            foreach (KeyValuePair<string, string> entry in postDict)
                            {
                                formDataContent.Add(new StringContent(entry.Value), entry.Key);
                            }
                            request.Content = formDataContent;
                        }
                        else
                        {
                            request.Content = new MultipartFormDataContent
                            {
                                { fileContent, "content", fileName }
                            };
                        }
                        var ret = await httpClient.SendAsync(request);
                        return ret;
                    }
                }
            }
            else if (filepathToDownload != null)
            {
                var ret = httpClient.SendAsync(request).Result;
                if (filepathToDownload != null)
                {
                    using (Stream contentStream = ret.Content.ReadAsStreamAsync().Result, fileStream = new FileStream(filepathToDownload, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                    {
                        await contentStream.CopyToAsync(fileStream);
                    }
                }
                return ret;
            }
            else { return null; }


        }
#if WINDOWS
        [System.Diagnostics.DebuggerStepThrough]
#endif
        public async static Task DownloadAsync(Models.DelelegateInfo Src, string SaveToFile)
        {
            await CreateRequestAsync(Src.Url, Src.Method, Src.Header, Src.data, SaveToFile, null);

        }
//#if WINDOWS
//        [System.Diagnostics.DebuggerStepThrough]
//#endif
        public async static Task UploadAsync(DelelegateInfo Dst, string UploadFile)
        {
            await CreateRequestAsync(Dst.Url, Dst.Method, Dst.Header, Dst.data, null, UploadFile);
        }
    }
}