using CodxClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.ServiceFactory
{
    public class ProcessService : Services.IProcessService
    {
        private Dictionary<string, Process> processList;

        public ProcessService() { 
            this.processList=new Dictionary<string, Process>();
            this.StartWatch();
        }

        private void StartWatch()
        {
            var run = new Task(() =>
            {
                foreach (var x in this.processList)
                {
                    if (x.Value.MainWindowHandle == 0)
                    {
                        x.Value.CloseMainWindow();
                        //x.Value.Close();
                        x.Value.Kill();
                        x.Value.Dispose();
                    }
                }
            });
        }

        public void ClearAll()
        {
            foreach( var x in this.processList)
            {
                x.Value.CloseMainWindow();
                //x.Value.Close();
                x.Value.Kill();
                x.Value.Dispose();
            }
        }

        public void KillProcessByRequestId(string RequestId)
        {
            if (this.processList.ContainsKey(RequestId))
            {
                this.processList[RequestId].CloseMainWindow();
                //this.processList[RequestId].Close();
                this.processList[RequestId].Kill();
                this.processList[RequestId].Dispose();
                this.processList.Remove(RequestId);
            }
        }

        public async Task ResovleAsync(RequestInfo Info, OfficeTools OfficeTool)
        {
            if (this.processList.ContainsKey(Info.RequestId))
            {
                this.processList[Info.RequestId].CloseMainWindow();
                //this.processList[Info.RequestId].Close();
                this.processList[Info.RequestId].Kill();
                this.processList[Info.RequestId].Dispose();
                this.processList.Remove(Info.RequestId);

            }
            Process process = Process.Start(OfficeTool.ExcutablePath, Info.FilePath);
            Info.Status = RequestInfoStatusEnum.Unknown;
            //process.Exited += Excel_Exited;
            //process.Disposed += Process_Disposed;
            processList.Add(Info.RequestId, process);
            await process.WaitForExitAsync();
            //Info.Applicaion = process;
        }

        //private void Process_Disposed(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void Excel_Exited(object sender, EventArgs e)
        //{
        //    var process= this.processList.AsParallel().FirstOrDefault(p => p.Value == sender);
        //    //throw new NotImplementedException();
        //}
    }
}
