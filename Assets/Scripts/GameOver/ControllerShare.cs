
using Commons.Services;
using System;
using System.Collections;
using UnityEngine;

public class ControllerShare: ControllerBaseGameOverElement
{
    protected override void RegisterClickHandler()
    {
        base.RegisterClickHandler();
        button.onClick.AddListener(OnClickShare);
    }

    private void OnClickShare()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        ServiceSharing.Instance.CaptureScreenshotNow();

        StopAllCoroutines();
        StartCoroutine(DelaySharing());
    }

    private IEnumerator DelaySharing()
    {
        yield return new WaitForSeconds(0.1f);
        ServiceSharing.Instance.Share();
    }
}
