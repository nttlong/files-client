namespace DeskCnn.Models
{
    /// <summary>
    /// "url":"http://172.16.7.99/lvfile/api/files/check_out_source",
    /// "method":"post",
    /// "header":{"Authorization":"Bearer eyJhbGciOiJIUzI1"},
    /// "data":{"appName":"developer","uploadId":"17d01338-f01f-4f64-abff-db32d4b3c4e4"}
    /// </summary>
    public class DelelegateInfo
    {
        public string? Url { get; set; }
        public string? Method { get; set; }
        public Dictionary<string, string>? Header { get; set; }
        public Dictionary<string, object>? data { get; set; }
        
    }
}