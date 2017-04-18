using UnityEngine;
using Commons.Singleton;
using UnityEngine.UI;
using System;

public class ControllerScore : MonoSingleton<ControllerScore> {

    [SerializeField] private Text score;
    [SerializeField] private Text highScore;
    [SerializeField] private Text bonusScore;
    [SerializeField] private Text coins;
    [SerializeField] private Text deaths;
    [SerializeField] private Image extraLife;

    public float currentScore { get; private set; }
    private new Camera camera;

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;

        UpdateScoreView();
        UpdateHighScore();
        UpdateCoins();
        UpdateDeaths();
        SetExtraLifeStatus(false);

        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        ControllerMainMenu.Instance.GameEnded += OnGameEnd;

        PlayerProfile.Instance.OnBestScoreUpdated += UpdateHighScore;
        PlayerProfile.Instance.OnDeathsUpdated += UpdateDeaths;
        PlayerProfile.Instance.OnCoinsUpdated += UpdateCoins;
    }

    //void FixedUpdate()
    //{
    //if (ControllerGame.Instance.IsGameOnGoing && !ControllerGame.Instance.IsGamePaused) {
    //    currentScore += Time.fixedDeltaTime;
    //    UpdateScoreView();
    //}
    //}

    private void UpdateCoins()
    {
        coins.text = "COINS: " + PlayerProfile.Instance.Coins;
    }

    private void UpdateDeaths()
    {
        deaths.text = "DEATHS: " + PlayerProfile.Instance.Deaths;
    }

    private void OnGameEnd()
    {
        PlayerProfile.Instance.UpdateBestScore(currentScore);
        highScore.gameObject.SetActive(true);
    }

    private void UpdateHighScore()
    {
        highScore.text = "BEST: " + PlayerProfile.Instance.BestScore.ToString(); // "0.00") + "s";
    }

    public void SetExtraLifeStatus(bool status)
    {
        //Debug.Log("extra life status: " + status);
        extraLife.gameObject.SetActive(status);
    }

    public void AddScore(int score = 1)
    {
        currentScore += score;
        UpdateScoreView();
    }

    public void BonusScore(Vector3 position, int reward)
    {
        Vector3 screenPos = camera.WorldToScreenPoint(position);
        screenPos.y += 40f;
        bonusScore.text = "+" + reward; // + "s";
        bonusScore.transform.position = screenPos;
        bonusScore.gameObject.SetActive(true);
        Invoke("DisableBonusScore", 1.5f);
    }

    private void DisableBonusScore()
    {
        bonusScore.gameObject.SetActive(false);
    }

    private void OnGameStarted()
    {
        currentScore = 0f;
        highScore.gameObject.SetActive(false);
        UpdateScoreView();
    }

    private void UpdateScoreView()
    {
        score.text = currentScore.ToString(); // "0.00 s");
    }

}
