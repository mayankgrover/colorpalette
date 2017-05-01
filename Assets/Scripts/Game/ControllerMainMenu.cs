using System;
using Commons.Singleton;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Commons.Services;

public class ControllerMainMenu : MonoSingleton<ControllerMainMenu>
{
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject playerStatsGroup;

    public Action GameStarted;
    public Action GameEnded;

    protected override void Awake()
    {
        base.Awake();
        playButton.onClick.AddListener(OnPlayClick);
    }

    private void OnPlayClick()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        if (GameStarted != null) GameStarted();
        Disable();
    }

    public void StartGame()
    {
        OnPlayClick();
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
