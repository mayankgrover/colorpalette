using UnityEngine;
using Commons.Singleton;
using UnityEngine.UI;
using System;

public class ControllerScore : MonoSingleton<ControllerScore> {

    [SerializeField] private Text score;
    [SerializeField] private Text bonusScore;
    //[SerializeField] private Text coins;
    [SerializeField] private Image extraLife;
    [SerializeField] private Camera canvasCamera;
    [SerializeField] private Camera gameCamera;

    private float currentScore;

    protected override void Start()
    {
        base.Start();
        UpdateScoreView();
        UpdateCoins();
        //SetExtraLifeStatus(false);

        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        //ControllerMainMenu.Instance.GameEnded += OnGameEnd;
    }

    private void UpdateCoins()
    {
        //coins.text = PlayerProfile.Instance.Coins + "c";
    }

    private void OnGameEnd()
    {
        Debug.Log("ControllerScore OnGameEnd");
        PlayerProfile.Instance.UpdateBestScore(currentScore);
    }

    public void SetExtraLifeStatus(bool status)
    {
        extraLife.gameObject.SetActive(status);
    }

    public void AddScore(int score = 1)
    {
        currentScore += score;
        UpdateScoreView();
        PlayerProfile.Instance.UpdateBestScore(currentScore);
    }

    public void BonusScore(Vector3 position, int reward)
    {
        Vector3 viewPortPos = gameCamera.WorldToViewportPoint(position);
        Vector3 canvasViewPoint = canvasCamera.ViewportToWorldPoint(viewPortPos);
        bonusScore.text = "+" + reward + "c";
        bonusScore.rectTransform.position = canvasViewPoint;
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
        UpdateScoreView();
    }

    private void UpdateScoreView()
    {
        PlayerProfile.Instance.UpdateScore(currentScore);
        score.text = currentScore.ToString();
    }

}
