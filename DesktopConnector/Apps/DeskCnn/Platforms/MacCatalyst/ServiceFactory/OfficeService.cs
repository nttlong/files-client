using DeskCnn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeskCnn.ServiceFactory
{
    public class OfficeService : Services.IOfficeService
    {
        public async Task<bool> OpenExcelAsync(RequestInfo info)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> OpenNotepadAsync(RequestInfo info)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> OpenPaintAsync(RequestInfo info)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> OpenPowerPointAsync(RequestInfo info)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> OpenWordAsync(RequestInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
