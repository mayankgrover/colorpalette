﻿using Commons.Ads;
using Commons.Popups;
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

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += GameStarted;
        ControllerMainMenu.Instance.GameEnded += GameEnded;
        ControllerEnemies.Instance.ClearedLevel += ClearedLevel;

        ColorsToUse = minColorsToUse;
    }

    private void ClearedLevel()
    {
        ColorsToUse += incColorsToUse;
        ColorsToUse = System.Math.Min(ColorsToUse, Colors.GameColors.Length);
        //Debug.Log("colors to use:" + ColorsToUse);
    }

    internal void PlayerRevived()
    {
        playerAlreadyRevived = true;
    }

    private void GameEnded()
    {
        IsGamePaused = false;
        IsGameOnGoing = false;
        ColorsToUse = minColorsToUse;
        ControllerScore.Instance.SetExtraLifeStatus(false);
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
        IsGameOnGoing = true;
        ColorsToUse = minColorsToUse;
        ControllerScore.Instance.SetExtraLifeStatus(IsExtraLifeAvailable());
    }

    private bool IsExtraLifeAvailable()
    {
        return PlayerProfile.Instance.AutoWatchAds && ServiceAds.Instance.IsRewardableAdReady();
             //ServiceAds.Instance.IsRewardableAdReady());
    }

    public void PlayerDied()
    {
        if( !playerAlreadyRevived && 
            ServiceAds.Instance.IsRewardableAdReady() && 
            PlayerProfile.Instance.AutoWatchAds)
        {
            playerAlreadyRevived = true;
            // force level clear 
            ControllerEnemies.Instance.ForceWaveClear();
            // vibrate the device.
            Handheld.Vibrate();
            // remove the heart
            ControllerScore.Instance.SetExtraLifeStatus(false);
            PauseGame();
            ControllerPause.Instance.Disable();
            Invoke("ResumeGameWithExtraLife", 2f);

            //PauseGame();
            //ControllerPopupManager.Instance.ShowPopup<ControllerPopupRevive>();
        } else {
            ControllerMainMenu.Instance.EndGame();
            ServiceAnalytics.Instance.ReportPlayerDied();
            if (PlayerProfile.Instance.AutoWatchAds && ServiceAds.Instance.IsRewardableAdReady()) {
                ServiceAds.Instance.ShowRewardableVideo(WatchAdAfterGameFinished);
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
        Debug.Log("[ReviveAd] result: " + result);
    }

    public void PauseGame()
    {
        IsGamePaused = true;
        if (GamePaused != null) GamePaused();
    }
}
