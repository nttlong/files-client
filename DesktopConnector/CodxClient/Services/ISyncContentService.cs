using CodxClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Services
{
    public interface ISyncContentService
    {
        public Task DoUploadContentAsync(RequestInfo requestInfo);
    }
}
