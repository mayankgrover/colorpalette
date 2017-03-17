
using System;
using Commons.Singleton;
using Commons.Ads;

public class ControllerGameOver : MonoSingleton<ControllerGameOver>
{
    private ControllerWatchAd controllerWatchAd;
    private ControllerUnlockGift controllerUnlockGift;
    private ControllerRateUs controllerRateUs;
    private ControllerNextGift controllerNextGift;

    protected override void Awake()
    {
        base.Awake();
        controllerWatchAd = GetComponentInChildren<ControllerWatchAd>(includeInactive: true);
        controllerUnlockGift = GetComponentInChildren<ControllerUnlockGift>(includeInactive: true);
        controllerRateUs = GetComponentInChildren<ControllerRateUs>(includeInactive: true);
        controllerNextGift = GetComponentInChildren<ControllerNextGift>(includeInactive: true);
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
    }

    private void OnGameEnded()
    {
        //controllerNextGift.Show();
        //if(ServiceAds.Instance.IsRewardableAdReady()) {
        //    controllerWatchAd.Show();
        //}
        if(PlayerProfile.Instance.GamesPlayed % 4 == 0) {
            controllerRateUs.Show();
        }
    }
}