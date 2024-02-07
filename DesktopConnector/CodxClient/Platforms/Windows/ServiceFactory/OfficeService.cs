using CodxClient.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Linq;
using CodxClient.Models;
using CodxClient.Libs;
namespace CodxClient.ServiceFactory
{
    public class OfficeService : Services.IOfficeService
    {
        private IToolDetectorService toolDetector;
        private IList<OfficeTools> listOfApps;

        public OfficeService() {
            this.toolDetector = ServiceAssistent.GetService<IToolDetectorService>();
            this.listOfApps = this.toolDetector.DoDetectOffice();
        }
        

        public async Task<bool> OpenExcelAsync(RequestInfo info)
        {
            info.Status = RequestInfoStatusEnum.Opening;
            var item = this.listOfApps.FirstOrDefault(p => p.ExcutableFile == "Excel.exe");
            Process Excel = Process.Start(item.ExcutablePath, info.FilePath);
            //Excel.CloseMainWindow();
            await Excel.WaitForExitAsync();
            info.Status = RequestInfoStatusEnum.Unknown;
            return true;
        }

        /// <summary>
        /// WORDPAD.EXE
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool OpenNotepad(string filePath)
        {
            try
            {
                var item = this.listOfApps.FirstOrDefault(p => p.ExcutableFile == "WORDPAD.EXE");

                Process Notepad = Process.Start(item.ExcutablePath, filePath);
                WinApi.SetForegroundWindow(Notepad.MainWindowHandle);
                Notepad.CloseMainWindow(); // Sends a close message to Excel
                Notepad.WaitForExit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Task<bool> OpenNotepadAsync(RequestInfo info)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// pbrush.exe
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool OpenPaint(string filePath)
        {

            try
            {
                var item = this.listOfApps.FirstOrDefault(p => p.ExcutableFile == "pbrush.exe");

                Process Excel = Process.Start(item.ExcutablePath, filePath);
                WinApi.SetForegroundWindow(Excel.MainWindowHandle);


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Task<bool> OpenPaintAsync(RequestInfo info)
        {
            throw new NotImplementedException();
        }

        public bool OpenPowerPoint(string filePath)
        {
            try
            {
                var item = this.listOfApps.FirstOrDefault(p => p.ExcutableFile == "Powerpnt.exe");

                Process Powerpnt = Process.Start(item.ExcutablePath, filePath);
                WinApi.SetForegroundWindow(Powerpnt.MainWindowHandle);


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Task<bool> OpenPowerPointAsync(RequestInfo info)
        {
            throw new NotImplementedException();
        }

        public bool OpenWord(string filePath)
        {
            try
            {
                var item = this.listOfApps.FirstOrDefault(p => p.ExcutableFile == "Winword.exe");

                Process Word = Process.Start(item.ExcutablePath, filePath);
                WinApi.SetForegroundWindow(Word.MainWindowHandle);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Task<bool> OpenWordAsync(RequestInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
