using Commons.Singleton;
using UnityEngine;

namespace Commons.Services
{
    [RequireComponent(typeof(NativeShare))]
    public class ServiceSharing: MonoSingleton<ServiceSharing>
    {
        public bool IsScreenshotAvailable { get; private set; }

        private NativeShare nativeShare;
        private static string storeLinkAndroid = "https://goo.gl/RIyVIB";
        private static string storeLinkApple = "https://goo.gl/LbgyY4";

        protected override void Awake()
        {
            base.Awake();
            nativeShare = GetComponent<NativeShare>();
        }

        private string storeLink =
#if UNITY_ANDROID
                storeLinkAndroid;
#elif UNITY_IOS
                storeLinkApple;
#endif

        public void Share(string text = "", string screenShotPath = "")
        {
            text = text != string.Empty ? text :
                string.Format(
                    StringConstants.SHARE_TEXT, PlayerProfile.Instance.CurrentScore, 
                    PlayerProfile.Instance.BestScore, storeLink
                );

            //Debug.Log("Sharing: " + text);
            nativeShare.ShareScreenshotWithText(text, screenShotPath);
        }

        public void CaptureScreenshotNow()
        {
            nativeShare.CaptureScreenshotNow();
            IsScreenshotAvailable = true;
        }
    }
}
