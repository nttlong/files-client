﻿using CodxClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodxClient.Services
{
    public interface INotificationService
    {
        void ShowNotification(string title, string body,bool silent);
        /// <summary>
        /// Create Notifier and return info
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="silent"></param>
        /// <returns></returns>
        object ShowNotificationWithWithProgressBar(string title, string body,string status, bool silent);
        void UpdateNotifier(object notifier, string key, string value);


    }
}
