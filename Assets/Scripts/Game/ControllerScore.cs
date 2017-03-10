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

    private float currentScore = 0;
    private new Camera camera;

    bool isGameActive = false;

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;

        UpdateScoreView();
        UpdateHighScore();
        UpdateCoins();
        UpdateDeaths();

        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        ControllerMainMenu.Instance.GameEnded += OnGameEnd;

        PlayerProfile.Instance.OnBestScoreUpdated += UpdateHighScore;
        PlayerProfile.Instance.OnDeathsUpdated += UpdateDeaths;
        PlayerProfile.Instance.OnCoinsUpdated += UpdateCoins;
    }

    void FixedUpdate()
    {
        if (isGameActive) {
            currentScore += Time.fixedDeltaTime;
            UpdateScoreView();
        }
    }

    private void UpdateCoins()
    {
        coins.text = "Coins: " + PlayerProfile.Instance.Coins;
    }

    private void UpdateDeaths()
    {
        deaths.text = "Deaths: " + PlayerProfile.Instance.Deaths;
    }

    private void OnGameEnd()
    {
        isGameActive = false;
        PlayerProfile.Instance.UpdateBestScore(currentScore);
        highScore.gameObject.SetActive(true);
    }

    private void UpdateHighScore()
    {
        highScore.text = "BEST: " + PlayerProfile.Instance.BestScore.ToString("0.00") + "s";
    }

    public void AddScore(int score = 1)
    {
        currentScore += score;
        UpdateScoreView();
    }

    public void BonusScore(Vector3 position, int reward)
    {
        Vector3 screenPos = camera.WorldToScreenPoint(position);
        bonusScore.text = "+" + reward + "c";
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
        isGameActive = true;
        currentScore = 0f;
        highScore.gameObject.SetActive(false);
        UpdateScoreView();
    }

    private void UpdateScoreView()
    {
        score.text = currentScore.ToString("0.00 s");
    }

}
