using CodxClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Services
{
    public interface IProcessService
    {
        void ClearAll();
        void KillProcessByRequestId(string requestId);
        Task ResovleAsync(RequestInfo info, OfficeTools item);
    }
}
