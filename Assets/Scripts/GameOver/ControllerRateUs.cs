using Commons.Services;
using UnityEngine;

public class ControllerRateUs: ControllerBaseGameOverElement
{
    protected override void RegisterClickHandler()
    {
        base.RegisterClickHandler();
        if(button != null) button.onClick.AddListener(OnClickRateUs);
    }

    public void OnClickRateUs()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        ServiceRateUs.Instance.RateNow();
    }
}