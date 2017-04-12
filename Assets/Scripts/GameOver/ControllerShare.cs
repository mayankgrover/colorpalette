
using Commons.Services;
using System;

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
        ServiceSharing.Instance.Share();
    }
}
