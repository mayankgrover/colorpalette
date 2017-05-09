
using System;
using Commons.Singleton;
using Commons.Services;
using UnityEngine;
using UnityEngine.UI;

public class ControllerGameOver : MonoSingleton<ControllerGameOver>
{
    private ControllerWatchAd controllerWatchAd;
    private ControllerUnlockGift controllerUnlockGift;
    private ControllerRateUs controllerRateUs;
    private ControllerNextGift controllerNextGift;
    private ControllerShare controllerShare;

    [SerializeField] private Button playButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Text score;
    [SerializeField] private Text highScore;

    protected override void Awake()
    {
        base.Awake();
        playButton.onClick.AddListener(onPlayClick);
        homeButton.onClick.AddListener(onClickHome);
        shopButton.onClick.AddListener(onClickShop);

        controllerWatchAd = GetComponentInChildren<ControllerWatchAd>(includeInactive: true);
        controllerUnlockGift = GetComponentInChildren<ControllerUnlockGift>(includeInactive: true);
        controllerRateUs = GetComponentInChildren<ControllerRateUs>(includeInactive: true);
        controllerNextGift = GetComponentInChildren<ControllerNextGift>(includeInactive: true);
        controllerShare = GetComponentInChildren<ControllerShare>(includeInactive: true);
    }

    private void onClickShop()
    {
        ControllerShop.Instance.Enable();
    }

    private void onClickHome()
    {
        Disable();
    }

    private void onPlayClick()
    {
        ControllerMainMenu.Instance.StartGame();
    }

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        ControllerMainMenu.Instance.GameEnded += OnGameEnded;
        Disable();
    }

    private void OnGameStarted()
    {
        controllerWatchAd.Hide();
        controllerUnlockGift.Hide();
        controllerRateUs.Hide();
        controllerNextGift.Hide();
        controllerShare.Hide();
        Disable();
    }

    private void UpdateScores()
    {
        score.text = PlayerProfile.Instance.CurrentScore.ToString();
        highScore.text = PlayerProfile.Instance.BestScore.ToString();
    }

    private void OnGameEnded()
    {
        //Debug.Log("ControllerGameOver OnGameEnd");
        controllerNextGift.Show();
        //if(ServiceAds.Instance.IsRewardableAdReady()) {
        //    controllerWatchAd.Show();
        //}

        UpdateScores();
        if (PlayerProfile.Instance.GamesPlayed % NumericConstants.GAMES_FOR_RATE_US_REMINDER == 0 &&
            controllerRateUs.IsAlreadyRated == false) {
            controllerRateUs.Show();
        } else controllerRateUs.Hide();

        // SS is now taken when the share button is clicked
        //if (ServiceSharing.Instance.IsScreenshotAvailable)
        {
            controllerShare.Show();
        }

        Enable();
    }
}