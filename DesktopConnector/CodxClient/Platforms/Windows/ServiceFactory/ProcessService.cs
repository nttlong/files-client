using CodxClient.Models;
using CodxClient.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.ServiceFactory
{
    public class ProcessService : Services.IProcessService
    {
        private ConcurrentDictionary<string,Tuple<Process,DateTime>> processList;
        private IRequestManagerService requestManagerService;
        private INotificationService notificationService;

        public ProcessService() { 
            this.processList=new ConcurrentDictionary<string, Tuple<Process, DateTime>>();
            this.requestManagerService= ServiceAssistent.GetService<IRequestManagerService>();
            this.notificationService = ServiceAssistent.GetService<INotificationService>(); 

            this.StartWatch();
        }

        private void StartWatch(double t=600)
        {
            var run = new Task(() =>
            {
                while (true)
                {
                    Task.Delay(100).Wait();
                    foreach(var x in this.processList)
                    {
                        
                        var (process, time) = x.Value;
                        if ((DateTime.UtcNow.Subtract(time).TotalSeconds > t) && (process.MainWindowHandle == 0))
                        {
                            try
                            {
                                process.CloseMainWindow();
                                //x.Value.Close();
                                process.Kill();
                                process.Dispose();
                            }
                            catch (Exception ex)
                            {
                                this.notificationService.ShowNotification("Error clear process", ex.Message, false);
                            }
                            this.processList.TryRemove(x);
                            requestManagerService.RemoveRequestByRequestId(x.Key);
                            this.notificationService.ShowNotification("Clear cache", $"{this.processList.Count}", false);
                            GC.Collect();
                        }
                        Task.Delay(100).Wait();
                    }
                    //var i = 0;
                    //while (i < this.processList.Count)
                    //{
                    //    var lst = this.processList.Keys.ToList();
                    //    var x = this.processList[lst[i]];
                    //    var (process, time) = x;
                    //    if ((DateTime.UtcNow.Subtract(time).TotalMinutes > t) && (process.MainWindowHandle == 0))
                    //    {
                    //        try
                    //        {
                    //            var info = this.requestManagerService.GetRequestInfoById(lst[i]);
                    //            if (!process.HasExited)
                    //            {
                    //                process.CloseMainWindow();
                    //                //x.Value.Close();
                    //                process.Kill();
                    //                process.Dispose();
                    //            }
                    //            if (info != null)
                    //            {
                    //                info.Delete();
                    //                this.requestManagerService.RemoveRequestByRequestId(lst[i]);
                    //            }
                    //        }
                    //        catch (Exception)
                    //        {

                    //        }
                    //        Task.Delay(1);

                    //    }
                    //    else
                    //    {
                    //        i++;
                    //    }
                    //}

                    //Task.Delay(1);
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
            if(this.processList.Count==0) return;
            if (this.processList.ContainsKey(RequestId))
            {
                
                Tuple<Process, DateTime> t = new Tuple<Process, DateTime>(null,DateTime.Now);
                this.processList.Remove(RequestId,out t);
                if ((t != null )&&(t.Item1!=null))
                {
                    try
                    {
                        t.Item1.Kill();
                        t.Item1.Dispose();
                    }
                    catch { }
                }
            }
        }

        public async Task ResovleAsync(RequestInfo Info, OfficeTools OfficeTool)
        {
            if (this.processList.ContainsKey(Info.RequestId))
            {
                Tuple<Process, DateTime> t = new Tuple<Process, DateTime>(null, DateTime.Now);
                this.processList.Remove(Info.RequestId, out t);
                if ((t != null) && (t.Item1 != null))
                {
                    t.Item1.Kill();
                    t.Item1.Dispose();
                }

            }
            Process process = Process.Start(OfficeTool.ExcutablePath, Info.FilePath);
            Info.Status = RequestInfoStatusEnum.Unknown;
            //process.Exited += Excel_Exited;
            //process.Disposed += Process_Disposed;
            processList.AddOrUpdate(Info.RequestId, (addKey) =>
            {
                return new Tuple<Process, DateTime>(process, DateTime.UtcNow);
            }, (updateKey, updateValue) =>
            {
                Tuple<Process, DateTime> oldValue = null;
                if (processList.TryGetValue(updateKey, out oldValue))
                {
                    if ((oldValue != null) && (oldValue.Item1 != null))
                    {
                        try
                        {
                            oldValue.Item1.Kill();
                            oldValue.Item1.Dispose();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                return new Tuple<Process, DateTime>(process, DateTime.UtcNow);

            }); ;
            Info.Status = RequestInfoStatusEnum.Ready;
            //processList.AddOrUpdate(Info.RequestId, new Tuple<Process, DateTime>(process,DateTime.UtcNow));
            //process.WaitForExitAsync().Wait();
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
