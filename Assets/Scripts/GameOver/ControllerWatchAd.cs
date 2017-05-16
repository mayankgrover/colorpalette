using Commons.Ads;
using Commons.Notification;
using Commons.Services;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class ControllerWatchAd : ControllerBaseGameOverElement
{
    private int coinsReward;

    protected override void SetText()
    {
        base.SetText();
        //text.text = StringConstants.GAMEOVER_WATCH_AD;
        text.text = "+" + coinsReward + "c";
    }

    protected override void RegisterClickHandler()
    {
        base.RegisterClickHandler();
        button.onClick.AddListener(OnClickWatchRewardableAd);
    }

    private void OnClickWatchRewardableAd()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        ServiceAds.Instance.ShowRewardableVideo(RewardableAdResult);
    }

    public override void Show()
    {
        base.Show();
        UpdateReward();
        SetText();
    }

    private void UpdateReward()
    {
        coinsReward = PlayerProfile.Instance.CoinsEarnedLastGame < 15 ?
                30 : 2 * PlayerProfile.Instance.CoinsEarnedLastGame;
    }

    private void RewardableAdResult(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Failed:
                Debug.Log("[Gameover] Loading Ad failed.");
                Hide();
                break;
            case ShowResult.Skipped:
                Debug.Log("[Gameover] Ad skipped, reward will not be given.");
                break;
            case ShowResult.Finished:
                Debug.Log("[Gameover] Ad watched, giving reward.");
                GivePlayerRewardForWatchingAd();
                //StartCoroutine(GivePlayerRewardForWatchingAd());
                break;
        }

        Hide();
    }

    private void GivePlayerRewardForWatchingAd()
    {
        //yield return new WaitForSecondsRealtime(1f);
        //int rewardCoins = UnityEngine.Random.Range(NumericConstants.MIN_REWARD_VIDEO_AD, NumericConstants.MAX_REWARD_VIDEO_AD);
        //int rewardCoins = PlayerProfile.Instance.CoinsEarnedLastGame;
        Debug.Log("Reward for watching ad: " + coinsReward);
        PlayerProfile.Instance.UpdateCoins(coinsReward);
        ControllerNotificationMessage.Instance.ShowMessage(coinsReward + "c rewarded!");
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.Game_Bonus);
    }
}
