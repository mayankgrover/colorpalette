using UnityEngine;
using Commons.Singleton;
using UnityEngine.UI;

public class ControllerScore : MonoSingleton<ControllerScore> {

    [SerializeField]
    private Text score;

    private int currentScore = 0;

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += ResetScore;
    }

    public void AddScore()
    {
        currentScore++;
        UpdateView();
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
