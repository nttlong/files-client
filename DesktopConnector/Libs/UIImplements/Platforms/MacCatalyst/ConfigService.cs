﻿using Foundation;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProviders;


namespace UIImplements
{
    public class ConfigService : IConfigService
    {
        private string appDataDir;
        private string builnumber;
        private string appName;
        private string appDataLocal;
        private string trackDir;
        private string contentDir;

        public string GetAppDataDir()
        {
            //macOS: ~/Library/Containers/<BundleIdentifier>/Data/Library/Application Support/<AppName> at debug
            if (appDataDir==null)
            {
                appDataDir= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Library", "Application Support");
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
            if (appName == null)
            {
                NSBundle mainBundle = NSBundle.MainBundle;
                appName = mainBundle.InfoDictionary["CFBundleName"].ToString();
            }
            return appName;
        }

        public string GetContentDir()
        {
            if(contentDir==null)
            {
                contentDir = Path.Combine(this.GetAppDataLocal(), "content");
                Directory.CreateDirectory(contentDir);
            }
            return contentDir;
        }

        public string GetTrackDir()
        {
            if(trackDir == null)
            {
                trackDir = Path.Combine(this.GetAppDataLocal(), "track");
                Directory.CreateDirectory(trackDir);
            }
            return trackDir;
        }

        public string GetVersion()
        {
            if (builnumber == null)
            {
                NSBundle mainBundle = NSBundle.MainBundle;
                string version = mainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();
                var _builnumber = mainBundle.InfoDictionary["CFBundleVersion"].ToString();
                builnumber = $"{version} ({_builnumber})";
            }
            return builnumber;
        }

        public void initialize()
        {
            this.GetAppDataLocal();
        }
    }
}
