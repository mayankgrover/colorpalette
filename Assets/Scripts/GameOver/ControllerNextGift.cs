
using System;
using UnityEngine;

public class ControllerNextGift: ControllerBaseGameOverElement
{
    private TimeSpan freeGiftDuration;
    private DateTime nextFreeGift;

    private bool isGiftReadyButNotClaimed = false;

    protected override void Start()
    {
        base.Start();
        freeGiftDuration = new TimeSpan(hours: 0, minutes: 0, seconds: NumericConstants.FREE_GIFT_CYCLE_SECONDS);
        nextFreeGift = new DateTime(PlayerProfile.Instance.FreeGiftTick + freeGiftDuration.Ticks);
        //Debug.Log("LastGift: " + (new DateTime(PlayerProfile.Instance.FreeGiftTick)).ToString());
        //Debug.Log("NextFreeGift: " + nextFreeGift.ToString());
        //Debug.Log("FreeGiftDuration: " + freeGiftDuration.ToString());
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
        return (span.Minutes + 1) + " mins";
    }

    private bool IsFreeGiftAvailable()
    {
        return nextFreeGift <= DateTime.Now;
    }

    private void OnClaimFreeGift()
    {
        isGiftReadyButNotClaimed = false;
        int reward = UnityEngine.Random.Range(NumericConstants.MIN_REWARD_FREE_GIFT, NumericConstants.MAX_REWARD_FREE_GIFT);
        //Debug.Log("[NextGift] Reward: " + reward + "c");
        //Debug.Log("[NextGift] now: " + DateTime.Now);
        PlayerProfile.Instance.UpdateCoins(reward);
        PlayerProfile.Instance.UpdateFreeGiftTimestamp(DateTime.Now.Ticks);
        ControllerScore.Instance.BonusScore(Vector3.zero, reward);
        ServiceAnalytics.Instance.ReportClaimFreeReward(true);
        nextFreeGift = DateTime.Now + freeGiftDuration;
        Hide();
    }
}
