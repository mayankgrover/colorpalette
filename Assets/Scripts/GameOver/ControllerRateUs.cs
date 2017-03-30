using System;
using UnityEngine;

public class ControllerRateUs: ControllerBaseGameOverElement
{
    protected override void RegisterClickHandler()
    {
        base.RegisterClickHandler();
        button.onClick.AddListener(OnClickRateUs);
    }

    private void OnClickRateUs()
    {
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=" + Application.bundleIdentifier);
        //Application.OpenURL("market://details?id=com.yodo1.crossyroad");
#endif
    }
}