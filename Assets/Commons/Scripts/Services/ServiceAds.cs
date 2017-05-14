
using System;
using Commons.Singleton;
using UnityEngine.Advertisements;
using UnityEngine;
using GoogleMobileAds.Api;

namespace Commons.Ads
{
    public class ServiceAds: MonoSingleton<ServiceAds>
    {
        public bool IsRewardableAdReady()
        {
            return Advertisement.IsReady(rewardedVideo) ||
                RewardBasedVideoAd.Instance.IsLoaded();
        }

        protected override void Awake()
        {
            base.Start();
            LoadRewardableVideoAd();
            RegisterAdMobEvents();
        }

        public void ShowRewardableVideo(Action<ShowResult> RewardableVideoResult)
        {
            if (RewardBasedVideoAd.Instance.IsLoaded() && ShowRewardableVideoAdMob(RewardableVideoResult)) {
                Debug.Log("Showing google admob video ad");
            }
            else if (Advertisement.IsReady(rewardedVideo) && ShowRewardableVideoUnity(RewardableVideoResult)) {
                Debug.Log("Showing unity video ad");
            }
            else {
                Debug.LogWarning("no ad available");
                RewardableVideoResult(ShowResult.Failed);
            }
        }

        #region UNITY ADS 

        private static string rewardedVideo = "rewardedVideo";
        private static string nonrewardVideo = "video";

        private bool ShowRewardableVideoUnity(Action<ShowResult> RewardableVideoResult)
        {
            if(Advertisement.IsReady(rewardedVideo)) {
                Advertisement.Show(rewardedVideo, new ShowOptions { resultCallback = RewardableVideoResult });
                return true;
            }
            return false;
        }

        //public bool IsNonRewardableAdReady() { return Advertisement.IsReady(nonrewardVideo); }
        //public void ShowNonRewardableVideo(Action<ShowResult> NonrewardableVideoResult)
        //{
        //    if(Advertisement.IsReady(nonrewardVideo)) {
        //        Advertisement.Show(nonrewardVideo, new ShowOptions{ resultCallback = NonrewardableVideoResult });
        //    } else {
        //        Debug.Log("[ServiceAds] Non-rewardable video ad not ready");
        //        if (NonrewardableVideoResult != null) NonrewardableVideoResult(ShowResult.Failed);
        //    }
        //}

        #endregion UNITY ADS 

        #region GOOGLE ADMOB

        private static string adMobEditorID = "unused";
        private static string adMobAndroidID = "ca-app-pub-9171749308188503/8071399671";
        private static string adMobAppleID   = "ca-app-pub-9171749308188503/3846349671";

        private Action<ShowResult> rewardableVideoResultCallback;

        private string GetAdUnitId()
        {
#if UNTIY_EDITOR
            return adMobEditorID;
#elif UNITY_ANDROID
            return adMobAndroidID;
#elif UNITY_IOS
            return adMobAppleID;
#endif
        }

        private void LoadRewardableVideoAd()
        {
            RewardBasedVideoAd.Instance.LoadAd(new AdRequest.Builder().Build(), GetAdUnitId());
        }

        private void RegisterAdMobEvents()
        {
            RewardBasedVideoAd.Instance.OnAdLoaded += OnVideoLoadedAdMob;
            RewardBasedVideoAd.Instance.OnAdStarted += OnVideoStartedAdMob;
            RewardBasedVideoAd.Instance.OnAdClosed += OnVideoAdClosedAdMob;
            RewardBasedVideoAd.Instance.OnAdFailedToLoad += OnVideoAdFailedAdMob;
            RewardBasedVideoAd.Instance.OnAdRewarded += OnVideoAdFinished;
        }

        private void OnVideoAdFinished(object sender, Reward e)
        {
            Debug.Log("AdMob video ad finished, give reward");
            LoadRewardableVideoAd();
            if (rewardableVideoResultCallback != null) {
                rewardableVideoResultCallback(ShowResult.Finished);
                rewardableVideoResultCallback = null;
            }
        }

        private void OnVideoAdFailedAdMob(object sender, AdFailedToLoadEventArgs e)
        {
            Debug.Log("AdMob video ad failed to load: " + e.Message);
            LoadRewardableVideoAd();
            if (rewardableVideoResultCallback != null) {
                rewardableVideoResultCallback(ShowResult.Failed);
            }
        }

        private void OnVideoAdClosedAdMob(object sender, EventArgs e)
        {
            Debug.Log("AdMob video ad closed");
            if (rewardableVideoResultCallback != null) {
                rewardableVideoResultCallback(ShowResult.Skipped);
                rewardableVideoResultCallback = null;
            }
        }

        private void OnVideoStartedAdMob(object sender, EventArgs e)
        {
            Debug.Log("AdMob video ad started playing");
        }

        private void OnVideoLoadedAdMob(object sender, EventArgs e)
        {
            Debug.Log("AdMob video ad loaded");
        }

        private bool ShowRewardableVideoAdMob(Action<ShowResult> RewardableVideoResult)
        {
            if(RewardBasedVideoAd.Instance.IsLoaded()) {
                rewardableVideoResultCallback = RewardableVideoResult;
                RewardBasedVideoAd.Instance.Show();
                return true;
            }
            return false;
        }

#endregion GOOGLE ADMOB
    }
}
