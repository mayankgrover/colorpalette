
using Commons.Notification;
using Commons.PushNotifications;
using Commons.Services;
using System;
using UnityEngine;

public class ControllerNextGift: ControllerBaseGameOverElement
{
    private TimeSpan freeGiftDuration;
    private DateTime nextFreeGift;

    private bool isGiftReadyButNotClaimed = false;

    protected override void Awake()
    {
        base.Awake();
        CancelFreeGiftPushNotification();
    }

    protected override void Start()
    {
        base.Start();
        freeGiftDuration = new TimeSpan(hours: 0, minutes: 0, seconds: NumericConstants.FREE_GIFT_CYCLE_SECONDS);
        nextFreeGift = new DateTime(PlayerProfile.Instance.FreeGiftTick + freeGiftDuration.Ticks);
        //Debug.Log("LastGift: " + (new DateTime(PlayerProfile.Instance.FreeGiftTick)).ToString());
        //Debug.Log("NextFreeGift: " + nextFreeGift.ToString());
        //Debug.Log("FreeGiftDuration: " + freeGiftDuration.ToString());
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause == true) {
            ScheduleFreeGiftPushNotification();
        } else {
            CancelFreeGiftPushNotification();
        }
    }

    private void OnApplicationQuit()
    {
        ScheduleFreeGiftPushNotification();
    }

    private void ScheduleFreeGiftPushNotification()
    {
        //Debug.Log("ScheduleFreeGiftPushNotification: " + GetNextGiftTimeDiff());
        ServiceLocalPushNotifications.Instance.ScheduleNotification(NumericConstants.PN_FREE_GIFT_ID,
            GetNextGiftTimeSpan(), StringConstants.PN_FREE_GIFT_TITLE, StringConstants.PN_FREE_GIFT_MSG);
    }

    private void CancelFreeGiftPushNotification()
    {
        //Debug.Log("CancelFreeGiftPushNotification");
        ServiceLocalPushNotifications.Instance.CancelNotification(NumericConstants.PN_FREE_GIFT_ID);
    }

    protected override void RegisterClickHandler()
    {
        base.RegisterClickHandler();
        button.onClick.AddListener(OnClaimFreeGift);
    }

    public override void Hide()
    {
        base.Hide();
        if (isGiftReadyButNotClaimed) ServiceAnalytics.Instance.ReportClaimFreeReward(false);
    }

    public override void Show()
    {
        base.Show();
        if(IsFreeGiftAvailable()) {
            isGiftReadyButNotClaimed = true;
            text.text = StringConstants.FREE_GIFT_AVAILABLE;
            button.gameObject.SetActive(true);
        } else {
            text.text = StringConstants.NEXT_FREE_GIFT + GetNextGiftTimeDiff();
            button.gameObject.SetActive(false);
        }
    }

    private string GetNextGiftTimeDiff()
    {
        TimeSpan span = (nextFreeGift - DateTime.Now);
        return span.Hours + "H " + (span.Minutes == 0 ? 1 : span.Minutes) + "M";
    }

    private TimeSpan GetNextGiftTimeSpan()
    {
        return (nextFreeGift - DateTime.Now);
    }

    public bool IsFreeGiftAvailable()
    {
        return nextFreeGift <= DateTime.Now;
    }

    private void OnClaimFreeGift()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.Game_Bonus);
        isGiftReadyButNotClaimed = false;
        int reward = UnityEngine.Random.Range(NumericConstants.MIN_REWARD_FREE_GIFT, NumericConstants.MAX_REWARD_FREE_GIFT);
        PlayerProfile.Instance.UpdateCoins(reward);
        PlayerProfile.Instance.UpdateFreeGiftTimestamp(DateTime.Now);
        ControllerNotificationMessage.Instance.ShowMessage(reward + "c rewarded!");
        ServiceAnalytics.Instance.ReportClaimFreeReward(true);
        nextFreeGift = DateTime.Now + freeGiftDuration;
        Show();
    }
}
