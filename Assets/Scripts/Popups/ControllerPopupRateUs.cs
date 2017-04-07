

using System;
using Commons.Popups;
using UnityEngine;
using UnityEngine.UI;

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
        throw new NotImplementedException();
    }
}
