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
        public bool OpenExcel(string filePath)
        {

            try
            {
                
                var item = this.listOfApps.FirstOrDefault(p => p.ExcutableFile == "Excel.exe");
                var fs = new Task(() =>
                {
                    Process Excel = Process.Start(item.Locate + "/excel.exe", filePath);
                });
                fs.Start();
                //WinApi.SetForegroundWindow(Excel.MainWindowHandle);

                //Excel.CloseMainWindow(); // Sends a close message to Excel
                //Excel.WaitForExit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
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
    }
}
