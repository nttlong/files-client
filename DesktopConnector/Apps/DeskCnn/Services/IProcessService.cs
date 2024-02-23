using DeskCnn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskCnn.Services
{
    public interface IProcessService
    {
        void ClearAll();
        void KillProcessByRequestId(string requestId);
        Task ResovleAsync(RequestInfo info, OfficeTools item);
    }
}
