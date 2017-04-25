using Commons.Singleton;
using UnityEngine;

namespace Commons.Services
{
    public class ServiceSharing: MonoSingleton<ServiceSharing>
    {
        [SerializeField] private NativeShare nativeShare;

        public bool IsScreenshotAvailable { get; private set; }

        private static string storeLinkAndroid = "https://goo.gl/RIyVIB";
        private static string storeLinkApple = "https://goo.gl/LbgyY4";

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
                    StringConstants.SHARE_TEXT, ControllerScore.Instance.currentScore, 
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
