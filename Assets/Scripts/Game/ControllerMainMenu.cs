using System;
using Commons.Singleton;
using UnityEngine;
using UnityEngine.UI;
using Commons.Ads;
using UnityEngine.Advertisements;

public class ControllerMainMenu : MonoSingleton<ControllerMainMenu>
{
    [SerializeField]
    private Button playButton;

    public Action GameStarted;
    public Action GameEnded;

    protected override void Awake()
    {
        base.Awake();
        playButton.onClick.AddListener(OnPlayClick);
    }

    private void OnPlayClick()
    {
        if (GameStarted != null) GameStarted();
        Disable();
    }

    public void EndGame()
    {
        if (GameEnded != null) GameEnded();
        Enable();
    }

    private void RewardableVideoResult(ShowResult rewardableVideoResult)
    {
        Debug.Log("RewardableVideoResult: " + rewardableVideoResult);
    }
}
