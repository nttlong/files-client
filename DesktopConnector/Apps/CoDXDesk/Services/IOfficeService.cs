using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoDXDesk.Services
{
    public interface IOfficeService
    {
        void OpenExcel(string filePath);
        void OpenWord(string filePath);
    }
}
