using System;
using UnityEngine;

namespace Commons.PushNotifications
{
    public class LocalAndroidPushNotifications
    {
        public enum NotificationExecuteMode
        {
            Inexact = 0,
            Exact = 1,
            ExactAndAllowWhileIdle = 2
        }

        private static string fullClassName = "com.unforgivinggames.androidnotifications.LocalPushNotification";
        private static string mainActivityClassName = "com.unity3d.player.UnityPlayerActivity";

        public static void SendNotification(int id, TimeSpan delay, string title, string message)
        {
            SendNotification(id, (int)delay.TotalSeconds, title, message, Color.grey);
        }

        public static void SendNotification(int id, long delay, string title, string message, Color32 bgColor, 
            bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "notification_icon_big", 
            string smallIcon = "notification_icon_small",
            NotificationExecuteMode executeMode = NotificationExecuteMode.Inexact)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
            if (pluginClass != null)
            {
                pluginClass.CallStatic("SetNotification", id, delay * 1000L, title, message, 
                    message, sound ? 1 : 0, vibrate ? 1 : 0, lights ? 1 : 0, bigIcon, smallIcon, 
                    bgColor.r * 65536 + bgColor.g * 256 + bgColor.b, (int)executeMode, mainActivityClassName);
            }
#endif
        }

        public static void SendRepeatingNotification(int id, long delay, long timeout, string title, 
            string message, Color32 bgColor, bool sound = true, bool vibrate = true, 
            bool lights = true, string bigIcon = "notification_icon_big", string smallIcon = "notification_icon_small")
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
            if (pluginClass != null)
            {
                pluginClass.CallStatic("SetRepeatingNotification", id, delay * 1000L, title, 
                    message, message, timeout * 1000, sound ? 1 : 0, vibrate ? 1 : 0, lights ? 1 : 0, 
                    bigIcon, smallIcon, bgColor.r * 65536 + bgColor.g * 256 + bgColor.b, 
                    mainActivityClassName);
            }
#endif
        }

        public static void CancelNotification(int id)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
            if (pluginClass != null)
            {
                pluginClass.CallStatic("CancelNotification", id);
            }
#endif
        }

        public static void CancelAllNotifications()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
            if (pluginClass != null)
            {
                pluginClass.CallStatic("CancelAll");
            }
#endif
        }
    }
}
