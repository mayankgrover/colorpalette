using Commons.Services;
using UnityEngine;

public class ControllerRateUs: ControllerBaseGameOverElement
{
    public string lastRatedVersion { get {
            return PlayerPrefs.GetString(StringConstants.RATE_US, "0" );
    }}

    public bool IsAlreadyRated { get { return lastRatedVersion == Application.version; } }

    protected override void RegisterClickHandler()
    {
        base.RegisterClickHandler();
        button.onClick.AddListener(OnClickRateUs);
    }

    public void OnClickRateUs()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        PlayerPrefs.SetString(StringConstants.RATE_US, Application.version);
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=" + Application.bundleIdentifier);
#elif UNITY_IOS
        Application.OpenURL("itms-apps://itunes.apple.com/app/id" + 1229341633);
#endif
    }
}