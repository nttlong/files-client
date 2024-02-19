using CodxClient.Models;
using CodxClient.Services;
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
        private Dictionary<string,Tuple<Process,DateTime>> processList;
        private IRequestManagerService requestManagerService;

        public ProcessService() { 
            this.processList=new Dictionary<string, Tuple<Process, DateTime>>();
            this.requestManagerService= ServiceAssistent.GetService<IRequestManagerService>();


            this.StartWatch();
        }

        private void StartWatch(double t=1)
        {
            var run = new Task(() =>
            {
                while (true)
                {
                    var lst = this.processList.Keys.ToList();
                    var i = 0;
                    while (i< lst.Count)
                    {
                        var x = this.processList[lst[i]];
                        var (process, time) = x;
                        if ((DateTime.UtcNow.Subtract(time).TotalMinutes > t) && (process.MainWindowHandle == 0))
                        {
                            var info = this.requestManagerService.GetRequestInfoById(lst[i]);
                            if (!process.HasExited)
                            {
                                process.CloseMainWindow();
                                //x.Value.Close();
                                process.Kill();
                                process.Dispose();
                            }
                            if (info != null)
                            {
                                info.Delete();
                                this.requestManagerService.RemoveRequestByRequestId(lst[i]);
                            }
                            Task.Delay(1);

                        }
                        else
                        {
                            i++;
                        }
                    }
                    
                    Task.Delay(1);
                }
                
            });
            run.Start();
        }

        public void ClearAll()
        {
            foreach( var x in this.processList)
            {
                var (process, time) = x.Value;
                var info = this.requestManagerService.GetRequestInfoById(x.Key);
                process.CloseMainWindow();
                //x.Value.Close();
                process.Kill();
                process.Dispose();
                if (info != null)
                {
                    info.Delete();
                }
            }
        }

        public void KillProcessByRequestId(string RequestId)
        {
            if (this.processList.ContainsKey(RequestId))
            {
                var (process,_)= this.processList[RequestId];
                process.CloseMainWindow();
                //this.processList[RequestId].Close();
                process.Kill();
                process.Dispose();
                this.processList.Remove(RequestId);
            }
        }

        public async Task ResovleAsync(RequestInfo Info, OfficeTools OfficeTool)
        {
            if (this.processList.ContainsKey(Info.RequestId))
            {
                var (_process, _) = this.processList[Info.RequestId];
                _process.CloseMainWindow();
                //this.processList[Info.RequestId].Close();
                _process.Kill();
                _process.Dispose();
                this.processList.Remove(Info.RequestId);

            }
            Process process = Process.Start(OfficeTool.ExcutablePath, Info.FilePath);
            Info.Status = RequestInfoStatusEnum.Unknown;
            //process.Exited += Excel_Exited;
            //process.Disposed += Process_Disposed;
            processList.Add(Info.RequestId, new Tuple<Process, DateTime>(process,DateTime.UtcNow));
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
