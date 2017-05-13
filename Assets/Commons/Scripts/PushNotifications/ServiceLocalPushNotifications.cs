using Commons.Singleton;
using System;
using UnityEngine;

namespace Commons.PushNotifications
{
    public class ServiceLocalPushNotifications: MonoSingleton<ServiceLocalPushNotifications>
    {
        protected override void Start()
        {
            base.Start();
            //TestLocalPushNotifications();
        }

        public void CancelNotification3()
        {
            LocalAndroidPushNotifications.CancelNotification(1003);
            Debug.Log("CancelNotification: 3");
        }

        public void FireNotification1()
        {
            LocalAndroidPushNotifications.SendNotification(1001, TimeSpan.FromSeconds(5), "(1) Notification",
                "Notification should fire in 10seconds from now");
            Debug.Log("SendNotification: 1");
        }

        public void FireNotification2()
        {
            LocalAndroidPushNotifications.SendNotification(1002, 5, "(2) Notification",
                "Notification should fire in 15seconds....", Color.black, bigIcon: "notification_icon_big");
            Debug.Log("SendNotification: 2");
        }

        public void FireNotification3()
        {
            LocalAndroidPushNotifications.SendNotification(1003, 5, "(3) Notification",
                "Notification should fire in 20seconds....", Color.white, bigIcon: "notification_icon_big");
            Debug.Log("SendNotification: 3");

        }

        public void FireNotification4()
        {
            LocalAndroidPushNotifications.SendRepeatingNotification(1004, 5, 3, "(4) Repeat Notification",
                "Notification should repeat every 8seconds..", Color.grey, smallIcon: "notification_icon_small");
            Debug.Log("SendNotification: 4");
        }
    }
}
