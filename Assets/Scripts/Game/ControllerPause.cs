
using Commons.Services;
using Commons.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPause: MonoSingleton<ControllerPause>
{
    [SerializeField] private Button btnPause;
    [SerializeField] private Button btnResume;
    [SerializeField] private GameObject panelResume;

    private bool isPaused = false;

    protected override void Awake()
    {
        base.Awake();
        btnPause.onClick.AddListener(OnClickPause);
        btnResume.onClick.AddListener(OnClickResume);
        //panelResume = btnResume.transform.parent.gameObject;
    }

    private void OnClickPause()
    {
        //Debug.Log("on click pause");
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        OnClick(paused: true);
    }

    private void OnClickResume()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        OnClick(paused: false);
    }

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        ControllerMainMenu.Instance.GameEnded += OnGameEnded;
        UpdateUI();
        panelResume.gameObject.SetActive(false);
    }

    private void OnApplicationPause(bool pause)
    {
        if(isPaused == false) {
            isPaused = true;
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
        panelResume.gameObject.SetActive(isPaused == true);
    }

    private void GamePaused()
    {
        Debug.Log("[ControllerPause] game paused:" + isPaused);
        UpdateUI();
        ControllerGame.Instance.PauseGame();
    }

    private void Reset()
    {
        isPaused = false;
        UpdateUI();
        Debug.Log("[ControllerPause] reset:" + isPaused);
    }
}
