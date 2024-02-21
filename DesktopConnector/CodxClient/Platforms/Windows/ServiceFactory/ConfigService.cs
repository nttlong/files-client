using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Windows.ApplicationModel;
namespace CodxClient.ServiceFactory
{
    public class ConfigService : Services.IConfigService
    {
        private string appDataDir;
        private string builnumber;
        private string appName;
        private string appDataLocal;
        private string trackDir;
        private string contentDir;
        private string tempDir;

        public string GetAppDataDir()
        {
            
            if(appDataDir == null)
            {
#if DEBUG
//%LOCALAPPDATA%\Packages\<PackageFamilyName>\LocalCache\Local\<AppName>
                var familyName = Package.Current.Id.FamilyName;
                appDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Packages", familyName, "LocalCache","Local");
#else
                appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
#endif

            }
            return appDataDir;
        }

        public string GetAppDataLocal()
        {
            if (appDataLocal == null)
            {
                appDataLocal = Path.Combine(this.GetAppDataDir(), this.GetAppName(), this.GetVersion());

                Directory.CreateDirectory(appDataLocal);
            }
            return appDataLocal;
        }

        public string GetAppName()
        {
            //if (appName == null)
            //{
            //    var package = Package.Current;
            //    var packageId = package.Id;

            //    appName = packageId.Name;
            //}
            //return appName;
            return "codx-desk";
        }

        public string GetVersion()
        {
            if (builnumber == null)
            {
                var package = Package.Current;
                var packageId = package.Id;

                var version = packageId.Version;
                builnumber = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";

                

            }
            return builnumber;
        }

        public void initialize()
        {
            this.GetAppDataLocal();
        }
        public string GetTrackDir()
        {
            if (trackDir == null)
            {
                trackDir = Path.Combine(this.GetAppDataLocal(), "track");
                Directory.CreateDirectory(trackDir);
            }
            return trackDir;
        }
        public string GetContentDir()
        {
            if (contentDir == null)
            {
                contentDir = Path.Combine(this.GetAppDataLocal(), "content");
                Directory.CreateDirectory(contentDir);
            }
            return contentDir;
        }

        public void SetAutoStartUp(bool IsAuto)
        {
            //App.Current.MainPage.DisplayAlert("test", Assembly.GetEntryAssembly().Location, "OK");
            //using (var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            //{
            //    if (IsAuto)
            //    {
            //        key.SetValue(Assembly.GetExecutingAssembly().GetName().Name,
            //                    Assembly.GetEntryAssembly().Location);
            //    }
            //    else
            //    {
            //        key.DeleteValue(Assembly.GetExecutingAssembly().GetName().Name, false);
            //    }
            //}
        }

        public string GetTempDir()
        {
            if (this.tempDir == null)
            {
                this.tempDir = Path.Combine(this.GetAppDataLocal(), "tmp");
                Directory.CreateDirectory(tempDir);
            } 
            return this.tempDir;
        }

        public long GetUploadBufferSize()
        {
            return 1024 * 10;
        }
        public long GetDownLoadBufferSize()
        {
            return 1024 * 10;
        }
    }
}
