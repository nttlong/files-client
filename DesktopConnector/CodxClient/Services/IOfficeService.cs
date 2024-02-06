using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Services
{
    public interface IOfficeService
    {
        bool OpenExcel(string filePath);
        bool OpenNotepad(string filePath);
        bool OpenPaint(string filePath);
        bool OpenPowerPoint(string filePath);
        bool OpenWord(string filePath);
    }
}
