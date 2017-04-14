using System;
using Commons.Popups;
using UnityEngine;
using UnityEngine.UI;
using Commons.Services;

public class ControllerPopupPromise: ControllerBasePopup
{
    [SerializeField] private Button btnPromise;

    protected override void RegisterClickHandlers()
    {
        base.RegisterClickHandlers();
        btnPromise.onClick.AddListener(OnClickPromise);
    }

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameEnded += OnGameEnded;
        Hide();
    }

    private void OnGameEnded()
    {
        //Debug.Log("Games played: " + PlayerProfile.Instance.GamesPlayed);
        if(PlayerProfile.Instance.GamesPlayed % NumericConstants.MIN_GAMES_FOR_WATCHING_ADS == 0 && 
           PlayerProfile.Instance.AutoWatchAds == false)
        {
            Show();
        }
    }

    private void OnClickPromise()
    {
        PlayerProfile.Instance.UpdateAutoWatchAds(true);
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        ServiceAnalytics.Instance.ReportAutoWatchAds(true);
        Hide();
    }

    protected override void OnClosePopup()
    {
        base.OnClosePopup();
        PlayerProfile.Instance.UpdateAutoWatchAds(false);
        ServiceAnalytics.Instance.ReportAutoWatchAds(false);
    }
}
