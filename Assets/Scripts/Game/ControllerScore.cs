using UnityEngine;
using Commons.Singleton;
using UnityEngine.UI;

public class ControllerScore : MonoSingleton<ControllerScore> {

    [SerializeField]
    private Text score;

    [SerializeField]
    private Text bonusScore;

    private int currentScore = 0;
    private Camera camera;

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
        ControllerMainMenu.Instance.GameStarted += ResetScore;
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
        UpdateView();
    }

    private void UpdateView()
    {
        score.text = "Score: " + currentScore;
    }

}
