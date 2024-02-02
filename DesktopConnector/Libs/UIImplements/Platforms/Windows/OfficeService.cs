using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProviders;
using Excel = NetOffice.ExcelApi;
using Word = NetOffice.WordApi;
namespace UIImplements
{
    public class OfficeService : IOfficeService
    {
        public void OpenExcel(string filePath)
        {
            Excel.Application wordApp = new Excel.Application();
            wordApp.Visible = true;

            Excel.Workbook doc = wordApp.Workbooks.Open(filePath);
            doc.Activate();
        }

        public void OpenWord(string filePath)
        {
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = true;

            Word.Document doc = wordApp.Documents.Open(filePath);
            doc.Activate();
        }
    }
}
