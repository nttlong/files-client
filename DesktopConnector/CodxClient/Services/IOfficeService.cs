using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Services
{
    public interface IOfficeService
    {
        void OpenExcel(string filePath);
        void OpenWord(string filePath);
    }
}
