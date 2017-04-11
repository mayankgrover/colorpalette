using Commons.Ads;
using Commons.Services;
using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class ControllerWatchAd : ControllerBaseGameOverElement
{
    protected override void SetText()
    {
        base.SetText();
        text.text = StringConstants.GAMEOVER_WATCH_AD;
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

    private void RewardableAdResult(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Failed:
                Debug.Log("[Gameover] Loading Ad failed.");
                break;
            case ShowResult.Skipped:
                Debug.Log("[Gameover] Ad skipped, reward will not be given.");
                break;
            case ShowResult.Finished:
                Debug.Log("[Gameover] Ad watched, giving reward.");
                GivePlayerRewardForWatchingAd();
                break;
        }

        Hide();
    }

    private void GivePlayerRewardForWatchingAd()
    {
        int rewardCoins = UnityEngine.Random.Range(NumericConstants.MIN_REWARD_VIDEO_AD, NumericConstants.MAX_REWARD_VIDEO_AD);
        Debug.Log("Reward for watching ad: " + rewardCoins);
        PlayerProfile.Instance.UpdateCoins(rewardCoins);
        ControllerScore.Instance.BonusScore(Vector3.zero, rewardCoins);
    }
}
