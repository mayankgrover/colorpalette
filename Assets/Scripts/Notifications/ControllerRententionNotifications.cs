

using Commons.PushNotifications;
using System;
using UnityEngine;

public class ControllerRententionNotifications: MonoBehaviour
{
    private void Start()
    {
        ScheduleDayNotification(NumericConstants.PN_DAY_2_ID, StringConstants.PN_DAY_2_TITLE, 
            StringConstants.PN_DAY_2_MSG, NumericConstants.PN_DAY_2);

        ScheduleDayNotification(NumericConstants.PN_DAY_3_ID, StringConstants.PN_DAY_3_TITLE, 
            StringConstants.PN_DAY_3_MSG, NumericConstants.PN_DAY_3);

        ScheduleDayNotification(NumericConstants.PN_DAY_5_ID, StringConstants.PN_DAY_5_TITLE, 
            StringConstants.PN_DAY_5_MSG, NumericConstants.PN_DAY_5);
    }

    private void ScheduleDayNotification(int id, string title, string msg, TimeSpan span)
    {
        ServiceLocalPushNotifications.Instance.ScheduleNotification( id, span, title, msg);
    }
}
