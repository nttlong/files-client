using CodxClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Win32;
namespace CodxClient.ServiceFactory
{
    public class ToolDetectorService : IToolDetectorService
    {
        public IList<Models.OfficeTools> DoDetectOffice()
        {
            var ret= new List<Models.OfficeTools>();
            Console.WriteLine("Checking for installed Office applications:");

            // Registry paths for common Office installations
            string[] officePaths = {
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Winword.exe",
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Excel.exe",
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Powerpnt.exe"
            };

            foreach (string path in officePaths)
            {
                string appName = path.Split('\\').Last().Split('.').First();

                try
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(path);
                    if (key != null)
                    {
                        string installPath = key.GetValue("(Default)") as string;
                        ret.Add(new Models.OfficeTools
                        {
                            AppName = path.Split(@"\")[path.Split(@"\").Length - 1],
                            Description="",
                            IsInstalled=true
                        });
                    }
                    else
                    {
                        ret.Add(new Models.OfficeTools
                        {
                            AppName = path.Split(@"\")[path.Split(@"\").Length - 1],
                            Description = "",
                            IsInstalled = false
                        });
                    }
                }
                catch (Exception ex)
                {
                    ret.Add(new Models.OfficeTools
                    {
                        AppName = path.Split(@"\")[path.Split(@"\").Length - 1],
                        Description = "",
                        IsInstalled = false
                    });
                }
            }
            bool visioInstalled = DetectAnyVersionInRegistry("visio", "Visio");
            bool msProjectInstalled = DetectAnyVersionInRegistry("mspprj", "MS Project");
            ret.Add(new Models.OfficeTools
            {
                AppName = "Visio",
                IsInstalled = visioInstalled
            });
            ret.Add(new Models.OfficeTools
            {
                AppName = "MS Project",
                IsInstalled = msProjectInstalled
            });
            return ret;
        }

        private bool DetectAnyVersionInRegistry(string appName, string basePath)
        {
            string[] subKeyNames = { "", "16.0", "17.0", "18.0", "19.0" }; // Example versions, adjust as needed

            foreach (string subKeyName in subKeyNames)
            {
                try
                {
                    string registryPath = basePath + appName + subKeyName + @"\DisplayName";
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath);

                    if (key != null)
                    {
                        string displayName = key.GetValue("") as string;
                        if (!string.IsNullOrEmpty(displayName))
                        {
                            return true; // Found a version, return true
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false; // Not found in any version
        }
    }
}
