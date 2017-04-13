using Commons.Singleton;
using UnityEngine;

namespace Commons.Services
{
    public class ServiceSharing: MonoSingleton<ServiceSharing>
    {
        [SerializeField] private NativeShare nativeShare;

        public bool IsScreenshotAvailable { get; private set; }

        // TODO add separate link for iOS 
        private string storeLink = "https://goo.gl/RIyVIB";

        public void Share(string text = "", string screenShotPath = "")
        {
            text = text != string.Empty ? text :
                string.Format(
                    StringConstants.SHARE_TEXT, ControllerScore.Instance.currentScore, 
                    PlayerProfile.Instance.BestScore, storeLink
                );

            Debug.Log("Sharing: " + text);
            nativeShare.ShareScreenshotWithText(text, screenShotPath);
        }

        public void CaptureScreenshotNow()
        {
            nativeShare.CaptureScreenshotNow();
            IsScreenshotAvailable = true;
        }
    }
}
