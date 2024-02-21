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
        private IProcessService processService;

        public OfficeService() {
            this.toolDetector = ServiceAssistent.GetService<IToolDetectorService>();
            this.listOfApps = this.toolDetector.DoDetectOffice();
            this.processService = ServiceAssistent.GetService<Services.IProcessService>();
        }
        

        public async Task<bool> OpenExcelAsync(RequestInfo info)
        {
            info.Status = RequestInfoStatusEnum.Opening;
            var item = this.listOfApps.FirstOrDefault(p => p.ExcutableFile.ToLower() == "excel.exe");
            if (item != null)
            {
                await this.processService.ResovleAsync(info, item);
                return true;
            }
            return false;
            
            
           
            //Excel.CloseMainWindow();
            
            
            return true;
        }
        /// <summary>
        /// WORDPAD.EXE
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> OpenNotepadAsync(RequestInfo info)
        {
            info.Status = RequestInfoStatusEnum.Opening;
            var item = this.listOfApps.FirstOrDefault(p =>(p.ExcutableFile!=null)&&(p.ExcutableFile.ToLower() == "notepad.exe".ToLower()));
            if (item != null)
            {
                await this.processService.ResovleAsync(info, item);
                return true;
            }
            return false;
        }

        
        
        /// <summary>
        /// pbrush.exe
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> OpenPaintAsync(RequestInfo info)
        {
            info.Status = RequestInfoStatusEnum.Opening;
            var item = this.listOfApps.FirstOrDefault(p => p.ExcutableFile.ToLower() == "pbrush.exe");
            if (item != null)
            {
                await this.processService.ResovleAsync(info, item);
                return true;
            }
            return false;
        }

        public async Task<bool> OpenPowerPointAsync(RequestInfo info)
        {
            info.Status = RequestInfoStatusEnum.Opening;
            var item = this.listOfApps.FirstOrDefault(p => p.ExcutableFile.ToLower() == "Powerpnt.exe".ToLower());
            if (item != null)
            {
                await this.processService.ResovleAsync(info, item);
                return true;
            }
            return false;
        }
        public async Task<bool> OpenWordAsync(RequestInfo info)
        {
            info.Status = RequestInfoStatusEnum.Opening;
            var item = this.listOfApps.FirstOrDefault(p => p.ExcutableFile.ToLower() == "Winword.exe".ToLower());
            if (item != null)
            {
                await this.processService.ResovleAsync(info, item);
                return true;
            }
            return false;
        }
    }
}
