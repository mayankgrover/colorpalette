using System;
using Commons.Singleton;
using Commons.Services;
using UnityEngine;
using UnityEngine.UI;
using Commons.Ads;

public class ControllerGameOver : MonoSingleton<ControllerGameOver>
{
    private ControllerWatchAd controllerWatchAd;
    //private ControllerUnlockGift controllerUnlockGift;
    private ControllerRateUs controllerRateUs;
    private ControllerNextGift controllerNextGift;
    private ControllerShare controllerShare;

    [SerializeField] private Button playButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Text score;
    [SerializeField] private Text highScore;
    [SerializeField] private Text coins;

    private bool alternateRateUs = false;

    protected override void Awake()
    {
        base.Awake();
        playButton.onClick.AddListener(onPlayClick);
        homeButton.onClick.AddListener(onClickHome);
        shopButton.onClick.AddListener(onClickShop);

        controllerWatchAd = GetComponentInChildren<ControllerWatchAd>(includeInactive: true);
        //controllerUnlockGift = GetComponentInChildren<ControllerUnlockGift>(includeInactive: true);
        controllerRateUs = GetComponentInChildren<ControllerRateUs>(includeInactive: true);
        controllerNextGift = GetComponentInChildren<ControllerNextGift>(includeInactive: true);
        controllerShare = GetComponentInChildren<ControllerShare>(includeInactive: true);
    }

    private void onClickShop()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        ControllerShop.Instance.Enable();
    }

    private void onClickHome()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        Disable();
    }

    private void onPlayClick()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        ControllerMainMenu.Instance.StartGame();
    }

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        ControllerMainMenu.Instance.GameEnded += OnGameEnded;
        PlayerProfile.Instance.OnCoinsUpdated += UpdateCoins;
        Disable();

    } 
    private void OnGameStarted()
    {
        controllerWatchAd.Hide();
        //controllerUnlockGift.Hide();
        controllerRateUs.Hide();
        controllerNextGift.Hide();
        controllerShare.Hide();
        Disable();
    }

    private void UpdateScores()
    {
        score.text = PlayerProfile.Instance.CurrentScore.ToString();
        highScore.text = "BEST: " + PlayerProfile.Instance.BestScore.ToString();
    }

    private void UpdateCoins()
    {
        coins.text = PlayerProfile.Instance.Coins + "c";
    }

    private void OnGameEnded()
    {
        UpdateScores();
        UpdateCoins();

        int elements = UnityEngine.Random.Range(1, 3);

        if (ServiceAds.Instance.IsRewardableAdReady() && PlayerProfile.Instance.GamesPlayed >= 3) {
            elements--;
            controllerWatchAd.Show();
        }

        if (PlayerProfile.Instance.GamesPlayed % NumericConstants.GAMES_FOR_RATE_US_REMINDER == 0 &&
            ServiceRateUs.Instance.IsAlreadyRated == false) {
            elements--;
            controllerRateUs.Show();
        }
        else {
            elements--;
            controllerShare.Show();
        }

        if (elements > 0 || controllerNextGift.IsFreeGiftAvailable())
        {
            elements--;
            controllerNextGift.Show();
        }

        alternateRateUs = !alternateRateUs;
        Enable();
    }
}