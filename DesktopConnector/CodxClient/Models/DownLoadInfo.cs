using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Models
{
    public class Info
    {
        public string? FilePath { get; set; }
        // public string AppName { get; set; }  // Commented out as instructed
        public string? FileNameOnly { get; set; }
        public string? FileExt { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int SizeInBytes { get; set; }
        public bool IsReady { get; set; }
        public bool IsSync { get; set; }
        public DateTime? ModifyTime { get; set; }
        public List<string>? HashContents { get; set; }
        public string? CheckOutUrl { get; set; }
        public string? CheckOutMethod { get; set; }
        public Dictionary<string, string>? CheckOutHeader { get; set; }
        public Dictionary<string, object>? CheckOutData { get; set; }
        public string? CheckInUrl { get; set; }
        public string? CheckInMethod { get; set; }
        public Dictionary<string, string>? CheckInHeader { get; set; }
        public Dictionary<string, object>? CheckInData { get; set; }
        public string? Url { get; set; }
        public string? Id { get; set; }
        public DownLoadInfoEnum Status { get; set; }
        public Dictionary<string, object>? ClientData { get; set; }
        public string? Host { get; set; }
        public string? Port { get; set; }
    }
}
