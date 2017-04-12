
using System;
using Commons.Singleton;
using Commons.Ads;
using Commons.Services;

public class ControllerGameOver : MonoSingleton<ControllerGameOver>
{
    private ControllerWatchAd controllerWatchAd;
    private ControllerUnlockGift controllerUnlockGift;
    private ControllerRateUs controllerRateUs;
    private ControllerNextGift controllerNextGift;
    private ControllerShare controllerShare;

    protected override void Awake()
    {
        base.Awake();
        controllerWatchAd = GetComponentInChildren<ControllerWatchAd>(includeInactive: true);
        controllerUnlockGift = GetComponentInChildren<ControllerUnlockGift>(includeInactive: true);
        controllerRateUs = GetComponentInChildren<ControllerRateUs>(includeInactive: true);
        controllerNextGift = GetComponentInChildren<ControllerNextGift>(includeInactive: true);
        controllerShare = GetComponentInChildren<ControllerShare>(includeInactive: true);
    }

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        ControllerMainMenu.Instance.GameEnded += OnGameEnded;
    }

    private void OnGameStarted()
    {
        controllerWatchAd.Hide();
        controllerUnlockGift.Hide();
        controllerRateUs.Hide();
        controllerNextGift.Hide();
        controllerShare.Hide();
    }

    private void OnGameEnded()
    {
        //controllerNextGift.Show();
        //if(ServiceAds.Instance.IsRewardableAdReady()) {
        //    controllerWatchAd.Show();
        //}

        if(PlayerProfile.Instance.GamesPlayed % NumericConstants.GAMES_FOR_RATE_US_REMINDER == 0 && 
            controllerRateUs.IsAlreadyRated == false) {
            controllerRateUs.Show();
        }

        if (ServiceSharing.Instance.IsScreenshotAvailable) {
            controllerShare.Show();
        }
    }
}