using CodxClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Services
{
    public interface IOfficeService
    {
        Task<bool> OpenExcelAsync(RequestInfo info);
        Task<bool> OpenNotepadAsync(RequestInfo info);
        Task<bool> OpenPaintAsync(RequestInfo info);
        Task<bool> OpenPowerPointAsync(RequestInfo info);
        Task<bool> OpenWordAsync(RequestInfo info);
    }
}
