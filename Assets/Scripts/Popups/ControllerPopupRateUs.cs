

using System;
using Commons.Popups;
using UnityEngine;
using UnityEngine.UI;
using Commons.Services;

public class ControllerPopupRateUs: ControllerBasePopup
{
    [SerializeField] private Button btnRateNow;

    protected override void RegisterClickHandlers()
    {
        base.RegisterClickHandlers();
        btnRateNow.onClick.AddListener(OnClickRate);
    }

    protected override void Start()
    {
        base.Start();
        Hide();
    }

    private void OnClickRate()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
    }
}
