using Commons.Ads;
using Commons.Popups;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Commons.Services;

public class ControllerPopupRevive: ControllerBasePopup
{
    [SerializeField] private Button watchAd;
    [SerializeField] private Text countdownTimer;

    private float timer = 0f;
    private bool isTimerActive = false;

    protected override void RegisterClickHandlers()
    {
        base.RegisterClickHandlers();
        watchAd.onClick.AddListener(OnClickWatchAd);
    }

    void Update()
    {
        if (isTimerActive) {
            timer -= Time.deltaTime;
            countdownTimer.text = (int)(timer + 1) + "";
        }

        if(timer <= -0.5f) {
            StopCountdownTimer();
            ControllerMainMenu.Instance.EndGame();
            ServiceAnalytics.Instance.ReportClickWatchAdToRevive(false);
        }
    }

    internal override void Show()
    {
        base.Show();
        StartCountdownTimer();
    }

    private void OnClickWatchAd()
    {
        StopCountdownTimer();
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        ServiceAnalytics.Instance.ReportClickWatchAdToRevive(true);
        ControllerGame.Instance.PlayerRevived();
        ControllerGame.Instance.ResumeGame();
        ControllerMainMenu.Instance.GameEnded += PlayVideoAdEndOfGame;
    }

    private void PlayVideoAdEndOfGame()
    {
        Debug.Log("playing video end of game for reviving in game");
        ServiceAds.Instance.ShowRewardableVideo(OnRewardableVideoWatched);
        ControllerMainMenu.Instance.GameEnded -= PlayVideoAdEndOfGame;
    }

    private void OnRewardableVideoWatched(ShowResult result)
    {
        Debug.Log("Finished watching ad for reviving during game: " + result);
    }

    private void StopCountdownTimer()
    {
        isTimerActive = false;
        timer = 0f;
        Hide();
    }

    private void StartCountdownTimer()
    {
        isTimerActive = true;
        timer = NumericConstants.REVIVE_COUNTDOWN_SECONDS;
    }
}