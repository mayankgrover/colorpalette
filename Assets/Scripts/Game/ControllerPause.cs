
using System;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPause: MonoBehaviour
{
    [SerializeField] private Button btnPause;
    [SerializeField] private Sprite imgPause;
    [SerializeField] private Sprite imgResume;

    private Image image;
    private bool isPaused = false;

    void Awake()
    {
        image = GetComponent<Image>();
        btnPause.onClick.AddListener(OnClickPause);
    }

    void Start()
    {
        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        ControllerMainMenu.Instance.GameEnded += OnGameEnded;
        UpdateUI();
        gameObject.SetActive(false);
    }

    private void OnGameEnded()
    {
        gameObject.SetActive(false);
    }

    private void OnGameStarted()
    {
        Reset();
        gameObject.SetActive(true);
    }

    private void OnClickPause()
    {
        isPaused = !isPaused;
        if (isPaused) GamePaused();
        else GameResumed();
    }

    private void GameResumed()
    {
        UpdateUI();
        ControllerGame.Instance.ResumeGame();
    }

    private void UpdateUI()
    {
        image.sprite = isPaused ? imgResume : imgPause;
    }

    private void GamePaused()
    {
        UpdateUI();
        ControllerGame.Instance.PauseGame();
    }

    private void Reset()
    {
        isPaused = false;
        UpdateUI();
    }
}
