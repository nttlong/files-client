using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoDXDesk
{
    public class DocSupports
    {

        public static Dictionary<string, string> AppWordMappingDict = new Dictionary<string, string>
        {
            {"doc", "Word.Application"},
            {"docx", "Word.Application"},
            {"dot", "Word.Application"},
            {"dotx", "Word.Application"},
            {"docm", "Word.Application"},
            {"dotm", "Word.Application"},
            {"rtf", "Word.Application"},
            {"odt", "Word.Application"},
            {"htm", "Word.Application"},
            {"html", "Word.Application"},
            {"wps", "Word.Application"},
            {"xml", "Word.Application"},
        };

        public static Dictionary<string, string> NotepadExtensions = new Dictionary<string, string>
        {
            {"txt", "notepad.exe"},
            {"log", "notepad.exe"},
            {"bat", "notepad.exe"},
            {"cmd", "notepad.exe"},
            {"ini", "notepad.exe"},
            {"inf", "notepad.exe"},
            {"reg", "notepad.exe"},
            {"csv", "notepad.exe"},
            {"html", "notepad.exe"},
            {"htm", "notepad.exe"},
            {"js", "notepad.exe"},
            {"css", "notepad.exe"},
            {"py", "notepad.exe"},
            {"vbs", "notepad.exe"},
            {"sql", "notepad.exe"},
            {"xml", "notepad.exe"},
            {"json", "notepad.exe"},
            {"config", "notepad.exe"},
            {"props", "notepad.exe"},
        };

        public static Dictionary<string, string> PowerpointExtensions = new Dictionary<string, string>
        {
            {"ppt", "PowerPoint.Application"},
            {"pptx", "PowerPoint.Application"},
            {"pot", "PowerPoint.Application"},
            {"potx", "PowerPoint.Application"},
            {"pps", "PowerPoint.Application"},
            {"ppsx", "PowerPoint.Application"},
            {"ppa", "PowerPoint.Application"},
            {"ppam", "PowerPoint.Application"},
            {"sldm", "PowerPoint.Application"},
            {"sldx", "PowerPoint.Application"},
            {"odp", "PowerPoint.Application"},
            {"thmx", "PowerPoint.Application"},
            {"potm", "PowerPoint.Application"},
            {"ppsm", "PowerPoint.Application"},
            {"pptm", "PowerPoint.Application"},
            {"sldxm", "PowerPoint.Application"},
        };

        public static Dictionary<string, string> ExcelExtensions = new Dictionary<string, string>
        {
            {"xls", "Excel.Application"},
            {"xlsx", "Excel.Application"},
            {"xltx", "Excel.Application"},
            {"xltm", "Excel.Application"},
            {"xlsm", "Excel.Application"},
            {"xlsb", "Excel.Application"},
            {"xlam", "Excel.Application"},
            {"ods", "Excel.Application"},
            {"csv", "Excel.Application"},
            {"tsv", "Excel.Application"},
            {"prn", "Excel.Application"},
            {"dif", "Excel.Application"},
            {"sylk", "Excel.Application"},
            {"xml", "Excel.Application"},
            {"htm", "Excel.Application"},
            {"html", "Excel.Application"},
            {"xps", "Excel.Application"},
        };

        public static Dictionary<string, string> PaintExtensions = new Dictionary<string, string>
        {
            {"bmp", "mspaint.exe"},
            {"gif", "mspaint.exe"},
            {"jpg", "mspaint.exe"},
            {"jpeg", "mspaint.exe"},
            {"png", "mspaint.exe"},
            {"tif", "mspaint.exe"},
            {"tiff", "mspaint.exe"},
            {"ico", "mspaint.exe"},
            {"jfif", "mspaint.exe"},
            {"dib", "mspaint.exe"},
            {"wdp", "mspaint.exe"},
            {"hdp", "mspaint.exe"},
            {"jxr", "mspaint.exe"},
            {"webp", "mspaint.exe"},
        };
        

    }
}
