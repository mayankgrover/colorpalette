
using Commons.Services;
using Commons.Singleton;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPause: MonoSingleton<ControllerPause>
{
    [SerializeField] private Button btnPause;
    [SerializeField] private Button btnResume;
    //[SerializeField] private Sprite imgPause;
    //[SerializeField] private Sprite imgResume;

    private GameObject panelResume;

    //private Image image;
    private bool isPaused = false;

    protected override void Awake()
    {
        base.Awake();
        //image = GetComponent<Image>();
        btnPause.onClick.AddListener(OnClickPause);
        btnResume.onClick.AddListener(OnClickResume);
        panelResume = btnResume.transform.parent.gameObject;
    }

    private void OnClickPause()
    {
        OnClick(paused: true);
    }

    private void OnClickResume()
    {
        OnClick(paused: false);
    }

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        ControllerMainMenu.Instance.GameEnded += OnGameEnded;
        UpdateUI();
        gameObject.SetActive(false);
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause == true) {
            isPaused = pause;
            GamePaused();
        }
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

    private void OnClick(bool paused)
    {
        isPaused = paused;
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
        //image.sprite = isPaused ? imgResume : imgPause;
        btnPause.gameObject.SetActive(isPaused == false);
        panelResume.gameObject.SetActive(isPaused == true);
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
