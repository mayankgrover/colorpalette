using System;
using Commons.Popups;
using UnityEngine;
using UnityEngine.UI;

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
        if(PlayerProfile.Instance.GamesPlayed == NumericConstants.MIN_GAMES_FOR_WATCHING_ADS) {
            Show();
        }
    }

    private void OnClickPromise()
    {
        PlayerProfile.Instance.UpdateAutoWatchAds(true);
        Hide();
    }

    protected override void OnClosePopup()
    {
        base.OnClosePopup();
        PlayerProfile.Instance.UpdateAutoWatchAds(false);
    }
}
