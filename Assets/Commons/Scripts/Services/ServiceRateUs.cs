using Commons.Singleton;
using UnityEngine;
using PlayerPrefs = ZPlayerPrefs;

namespace Commons.Services
{
    public class ServiceRateUs: MonoSingleton<ServiceRateUs>
    {
        public string LastRatedVersion { get {
            return PlayerPrefs.GetString(StringConstants.RATE_US, "0" );
        }}

        public bool IsAlreadyRated { get { return LastRatedVersion == Application.version; } }

        public void RateNow()
        {
            PlayerPrefs.SetString(StringConstants.RATE_US, Application.version);
            #if UNITY_ANDROID
                Application.OpenURL("market://details?id=" + Application.identifier);
            #elif UNITY_IOS
                Application.OpenURL("itms-apps://itunes.apple.com/app/id" + 1229341633);
            #endif
        }
    }
}
