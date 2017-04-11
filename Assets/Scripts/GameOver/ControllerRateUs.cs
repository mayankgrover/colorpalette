using Commons.Services;
using System;
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
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=" + Application.bundleIdentifier);
        PlayerPrefs.SetString(StringConstants.RATE_US, Application.version);
#endif
    }
}