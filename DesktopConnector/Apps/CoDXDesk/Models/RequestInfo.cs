namespace CoDXDesk.Models
{
    /// <summary>
    /// {
    /// "resourceExt":"pdf",
    /// "src":{"url":"http://172.16.7.99/lvfile/api/files/check_out_source",
    /// "method":"post",
    /// "header":{"Authorization":"Bearer eyJhbGciOiJIUzI1"},
    /// "data":{"appName":"developer","uploadId":"17d01338-f01f-4f64-abff-db32d4b3c4e4"}},
    /// "dst":{"url":"http://172.16.7.99/lvfile/api/files/check_in_source",
    /// "method":"post","header":{"Authorization":"Bearer eyJh"},
    /// "data":{"appName":"developer","uploadId":"17d01338-f01f-4f64-abff-db32d4b3c4e4"}}}	
    /// </summary>
    public class RequestInfo
    {
        public DelelegateInfo? Src { get; set; }
        public DelelegateInfo? Dst { get; set; }
        public string? ResourceExt { get; set; }
        public string? HashContent { get; set; }
        public string? FilePath { get; set; }
        public string? TrackFilePath { get; set; }
        public string? RequestId { get; set; }
    }
}