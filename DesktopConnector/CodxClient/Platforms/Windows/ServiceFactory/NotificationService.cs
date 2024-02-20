﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodxClient.Models;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;

namespace CodxClient.ServiceFactory
{
    public class NotificationService : Services.INotificationService
    {
        public void ShowNotification(string title,string body, bool silent=true)
        {
            var ret = new ToastContentBuilder()
            .AddText(title)
            .AddText(body);
            if (silent)
            {
                ret.AddAudio(new Uri("ms-appx:///Assets/silent.wav"), silent: true);
            }
            ret.Show();
            
            
        }



        public object ShowNotificationWithWithProgressBar(string title, string body, string status, bool silent)
        {
            // Define a tag (and optionally a group) to uniquely identify the notification, in order update the notification data later;
            //string tag = "weekly-playlist";
            //string group = "downloads";

            // Construct the toast content with data bound fields
            var content = new ToastContentBuilder();
            content.AddText(body);
            content.AddVisualChild(new AdaptiveProgressBar()
                {
                    Title = title,
                    Value = new BindableProgressBarValue("progressValue"),
                    ValueStringOverride = new BindableString("progressValueString"),
                    Status = new BindableString("progressStatus")
                    
                });

            if (silent)
            {
                content.AddAudio(new Uri("ms-appx:///Assets/silent.wav"), silent: true);
            }
            // Generate the toast notification
            var toast = new ToastNotification(content.GetToastContent().GetXml());

            // Assign the tag and group
            toast.Tag =  (new Guid()).ToString();
           
            
            // Assign initial NotificationData values
            // Values must be of type string
            toast.Data = new NotificationData();
            toast.Data.Values["progressValue"] = "0";
            toast.Data.Values["progressValueString"] = "";
            toast.Data.Values["progressStatus"] = status;
            
            // Provide sequence number to prevent out-of-order updates, or assign 0 to indicate "always update"
            //toast.Data.SequenceNumber = 1;
            
            

            // Show the toast notification to the user
            var ret = ToastNotificationManager.CreateToastNotifier();
            
            toast.Dismissed += (sender, args) =>
            {
                
            };

            ret.Show(toast);
            
            return new Tuple<object,object>(ret,toast);
        }

        

        public void UpdateNotifier(object notifier, string key, string value)
        {
            if (notifier.GetType() == typeof(Tuple<object, object>))
            {
                var tup = (Tuple<object, object>)notifier;
                if ((tup.Item1 != null) && (tup.Item2 != null))
                {
                    if ((tup.Item1.GetType() == typeof(ToastNotifier)) && (tup.Item2.GetType() == typeof(ToastNotification)))
                    {
                        var toastNotifier = tup.Item1 as ToastNotifier;
                        var toastNotification = tup.Item2 as ToastNotification;
                        if (!toastNotification.Data.Values.ContainsKey(key))
                        {
                            throw new Exception($"The key {key} was not found.");
                        }
                        toastNotification.Data.Values[key] = value;

                        toastNotifier.Update(toastNotification.Data, toastNotification.Tag);

                    }
                }
            }
        }

        
    }
}
