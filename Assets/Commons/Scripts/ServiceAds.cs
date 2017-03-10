
using System;
using Commons.Singleton;
using UnityEngine.Advertisements;
using UnityEngine;

namespace Commons.Ads
{
    public class ServiceAds: MonoSingleton<ServiceAds>
    {
        public bool IsRewardableAdReady() { return Advertisement.IsReady(rewardedVideo); }
        public bool IsNonRewardableAdReady() { return Advertisement.IsReady(nonrewardVideo); }

        private static string rewardedVideo = "rewardedVideo";
        private static string nonrewardVideo = "video";

        public void ShowRewardableVideo(Action<ShowResult> RewardableVideoResult)
        {
            if(Advertisement.IsReady(rewardedVideo)) {
                Advertisement.Show(rewardedVideo, new ShowOptions { resultCallback = RewardableVideoResult });
            } else {
                Debug.Log("[ServiceAds] RewardableVideo not ready");
                if (RewardableVideoResult != null) RewardableVideoResult(ShowResult.Failed);
            }
        }

        public void ShowNonRewardableVideo(Action<ShowResult> NonrewardableVideoResult)
        {
            if(Advertisement.IsReady(nonrewardVideo)) {
                Advertisement.Show(nonrewardVideo, new ShowOptions{ resultCallback = NonrewardableVideoResult });
            } else {
                Debug.Log("[ServiceAds] Non-rewardable video ad not ready");
                if (NonrewardableVideoResult != null) NonrewardableVideoResult(ShowResult.Failed);
            }
        }
    }
}
