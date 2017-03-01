using UnityEngine;
using Commons.Singleton;
using UnityEngine.UI;
using System;

public class ControllerScore : MonoSingleton<ControllerScore> {

    [SerializeField]
    private Text score;
    [SerializeField]
    private Text highScore;
    [SerializeField]
    private Text bonusScore;

    private int currentScore = 0;
    private Camera camera;

    private const string High_Score = "High_Score";

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
        UpdateHighScore(PlayerPrefs.GetInt(High_Score, 0));
        ControllerMainMenu.Instance.GameStarted += ResetScore;
        ControllerMainMenu.Instance.GameEnded += OnGameEnd;
    }

    private void OnGameEnd()
    {
        int prevHigh = PlayerPrefs.GetInt(High_Score, 0);
        Debug.Log("Prev: " + prevHigh + " current:" + currentScore);
        if(currentScore > prevHigh) {
            UpdateHighScore(currentScore);
        }
        highScore.gameObject.SetActive(true);
    }

    private void UpdateHighScore(int currentScore)
    {
        PlayerPrefs.SetInt(High_Score, currentScore);
        highScore.text = "High Score: " + currentScore;
    }

    public void AddScore(int score = 1)
    {
        currentScore += score;
        UpdateView();
    }

    public void BonusScore(Vector3 position)
    {
        Vector3 screenPos = camera.WorldToScreenPoint(position);
        //Debug.Log("Pos: " + position + " screenPos:" + screenPos);
        bonusScore.transform.position = screenPos;
        bonusScore.gameObject.SetActive(true);
        Invoke("DisableBonusScore", 1f);
    }

    private void DisableBonusScore()
    {
        bonusScore.gameObject.SetActive(false);
    }

    public void ResetScore()
    {
        currentScore = 0;
        highScore.gameObject.SetActive(false);
        UpdateView();
    }

    private void UpdateView()
    {
        score.text = "Score: " + currentScore;
    }

}
