using System;
using Commons.Singleton;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Commons.Services;

public class ControllerMainMenu : MonoSingleton<ControllerMainMenu>
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button shopButton;

    [SerializeField] private Text score;
    [SerializeField] private Text coins;
    [SerializeField] private Text highScore;

    public Action GameStarted;
    public Action GameEnded;

    protected override void Awake()
    {
        base.Awake();
        playButton.onClick.AddListener(onClickPlay);
        shopButton.onClick.AddListener(onClickShop);

        OnEnabled += UpdateUI;
    }

    protected override void Start()
    {
        base.Start();

        PlayerProfile.Instance.OnBestScoreUpdated += UpdateUI;
        PlayerProfile.Instance.OnDeathsUpdated += UpdateUI;
        PlayerProfile.Instance.OnCoinsUpdated += UpdateUI;
        UpdateUI();
    }

    private void onClickShop()
    {
        ControllerShop.Instance.Enable();
    }

    private void onClickPlay()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        if (GameStarted != null) GameStarted();
        Disable();
    }

    public void StartGame()
    {
        onClickPlay();
    }

    public void EndGame()
    {
        if (GameEnded != null) GameEnded();
        Enable();
    }
    
    private void UpdateUI()
    {
        coins.text = PlayerProfile.Instance.Coins.ToString() + "c";
        score.text = PlayerProfile.Instance.CurrentScore.ToString();
        highScore.text = "BEST: " + PlayerProfile.Instance.BestScore.ToString();  
    }

    private void RewardableVideoResult(ShowResult rewardableVideoResult)
    {
        Debug.Log("RewardableVideoResult: " + rewardableVideoResult);
    }
}
