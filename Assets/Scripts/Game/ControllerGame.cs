using Commons.Ads;
using Commons.Singleton;
using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class ControllerGame: MonoSingleton<ControllerGame>
{
    public int ColorsToUse { get; private set; }
    public bool IsGamePaused { get; private set; }
    public bool IsGameOnGoing { get; private set; }

    public Action GamePaused;
    public Action GameResumed;

    private int minColorsToUse = 3;
    private int incColorsToUse = 1;

    private bool playerAlreadyRevived = false;

    protected override void Awake()
    {
        base.Awake();
        Input.backButtonLeavesApp = true;
        Application.targetFrameRate = 60;
    }

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += GameStarted;
        ControllerMainMenu.Instance.GameEnded += GameEnded;
        ControllerEnemies.Instance.ClearedLevel += ClearedLevel;

        ColorsToUse = minColorsToUse;
        Disable();
    }

    private void ClearedLevel()
    {
        ColorsToUse += incColorsToUse;
        ColorsToUse = System.Math.Min(ColorsToUse, Colors.Instance.GameColors.Length);
        //Debug.Log("colors to use:" + ColorsToUse);
    }

    internal void PlayerRevived()
    {
        playerAlreadyRevived = true;
    }

    private void GameEnded()
    {
        //Debug.Log("ControllerGame OnGameEnd");
        IsGamePaused = false;
        IsGameOnGoing = false;
        ColorsToUse = minColorsToUse;
        ControllerScore.Instance.SetExtraLifeStatus(false);
        Disable();
    }

    public void ResumeGame()
    {
        IsGamePaused = false;
        if (GameResumed != null) GameResumed();
    }

    private void GameStarted()
    {
        //UnityEngine.Random.seed = (int) DateTime.UtcNow.Date.Ticks;
        //Debug.Log("using seed: " + UnityEngine.Random.seed);
        playerAlreadyRevived = false;
        IsGamePaused = false;
        IsGameOnGoing = true;
        ColorsToUse = minColorsToUse;
        ControllerScore.Instance.SetExtraLifeStatus(IsExtraLifeAvailable());
        Enable();
    }

    private bool IsExtraLifeAvailable()
    {
        return PlayerProfile.Instance.AutoWatchAds && ServiceAds.Instance.IsRewardableAdReadyUnity();
    }

    public void PlayerDied()
    {
        if( !playerAlreadyRevived && 
            ServiceAds.Instance.IsRewardableAdReadyUnity() && 
            PlayerProfile.Instance.AutoWatchAds)
        {
            playerAlreadyRevived = true;
            ControllerEnemies.Instance.ForceWaveClear();
            ControllerScore.Instance.SetExtraLifeStatus(false);
            PauseGame();
            ControllerPause.Instance.Disable();
            Invoke("ResumeGameWithExtraLife", 2f);
        } else {

            if (PlayerProfile.Instance.AutoWatchAds && ServiceAds.Instance.IsRewardableAdReadyUnity()) {
                ServiceAds.Instance.ShowRewardableVideo(WatchAdAfterGameFinished);
            }
            else {
                ControllerMainMenu.Instance.EndGame();
                ServiceAnalytics.Instance.ReportPlayerDied(false);
            }
        }
    }

    private void ResumeGameWithExtraLife()
    {
        ResumeGame();
        ControllerPause.Instance.Enable();
    }

    private void WatchAdAfterGameFinished(ShowResult result)
    {
        //Debug.Log("[ReviveAd] result: " + result);
        ControllerMainMenu.Instance.EndGame();
        ServiceAnalytics.Instance.ReportPlayerDied(result == ShowResult.Finished);
    }

    public void PauseGame()
    {
        IsGamePaused = true;
        if (GamePaused != null) GamePaused();
    }
}
