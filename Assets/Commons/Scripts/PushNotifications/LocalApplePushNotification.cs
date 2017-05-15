#if UNITY_IOS
using System;
using System.Collections.Generic;
using UnityEngine.iOS;

namespace Commons.PushNotifications
{
    public class LocalApplePushNotification
    {
        public static void SendNotification(int id, TimeSpan delay, string title, string message)
        {
            LocalNotification notification = new LocalNotification();
            notification.alertBody = message;
            notification.fireDate = DateTime.Now + delay;
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("id", id);
            notification.userInfo = data;
            NotificationServices.ScheduleLocalNotification(notification);
        }

        public static void CancelNotification(int id)
        {
            LocalNotification[] notifications = NotificationServices.scheduledLocalNotifications;
            for(int i = 0; i < notifications.Length; i++)
            {
                LocalNotification notification = notifications[i];
                if(notification.userInfo.Contains("id") && 
                    (int)notification.userInfo["id"] == id)
                {
                    NotificationServices.CancelLocalNotification(notification);
                }
            }
        }
    }
}
#endif
