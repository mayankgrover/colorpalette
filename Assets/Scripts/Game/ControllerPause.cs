
using Commons.Services;
using Commons.Singleton;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPause: MonoSingleton<ControllerPause>
{
    [SerializeField] private Button btnPause;
    [SerializeField] private Sprite imgPause;
    [SerializeField] private Sprite imgResume;

    private Image image;
    private bool isPaused = false;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        btnPause.onClick.AddListener(OnClickPause);
    }

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        ControllerMainMenu.Instance.GameEnded += OnGameEnded;
        UpdateUI();
        gameObject.SetActive(false);
    }

    private void OnGameEnded()
    {
        Disable();
    }

    private void OnGameStarted()
    {
        Reset();
        Enable();
    }

    private void OnClickPause()
    {
        isPaused = !isPaused;
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
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
