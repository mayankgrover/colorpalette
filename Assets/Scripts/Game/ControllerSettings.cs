
using System;
using Commons.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSettings: MonoSingleton<ControllerSettings>
{
    [SerializeField] private Toggle autoWatchAd;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnInitialized()
    {
        base.OnInitialized();
        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        ControllerMainMenu.Instance.GameEnded += OnGameEnded;
        PlayerProfile.Instance.OnAutoWatchAdsUpdated += OnAutoWatchAdUpdated;
    }

    private void OnAutoWatchAdUpdated()
    {
        autoWatchAd.onValueChanged.RemoveListener(OnClickAutoWatchAd);
        UpdateUI(PlayerProfile.Instance.AutoWatchAds);
        autoWatchAd.onValueChanged.AddListener(OnClickAutoWatchAd);
    }

    private void OnGameEnded()
    {
        UpdateUI(PlayerProfile.Instance.AutoWatchAds);
    }

    private void OnGameStarted()
    {
        gameObject.SetActive(false);
    }

    private void OnClickAutoWatchAd(bool status)
    {
        Debug.Log("auto watch on click event");
        //UpdateUI(status);
        PlayerProfile.Instance.UpdateAutoWatchAds(status);
    }

    protected override void Start()
    {
        base.Start();
        UpdateUI(PlayerProfile.Instance.AutoWatchAds);
        autoWatchAd.onValueChanged.AddListener(OnClickAutoWatchAd);
    }

    private void UpdateUI(bool status)
    {
        gameObject.SetActive(PlayerProfile.Instance.GamesPlayed > NumericConstants.MIN_GAMES_FOR_WATCHING_ADS);
        autoWatchAd.isOn = status;
    }

    public override void Enable()
    {
        base.Enable();
        UpdateUI(PlayerProfile.Instance.AutoWatchAds);
    }
}
