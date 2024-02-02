using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProviders;
using Windows.ApplicationModel.Core;
using NetOffice;
using Word = NetOffice.WordApi;
namespace UIImplements
{
    public class WordService : IWordService
    {
        public void OpenFile(string filePath)
        {
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = true;

            Word.Document doc = wordApp.Documents.Open(filePath);
            doc.Activate();
        }
    }
}
