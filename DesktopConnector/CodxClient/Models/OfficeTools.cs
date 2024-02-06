using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Models
{
    public class OfficeTools
    {
        public string AppName { get; internal set; }
        public string Description { get; internal set; }
        public bool IsInstalled { get; internal set; }
        public string ImageUrl { get; set; }
        public object Locate { get; internal set; }
        public string ExcutableFile { get; internal set; }
        public string ExcutablePath { get; internal set; }
        public string AppId { get; internal set; }
    }
}
