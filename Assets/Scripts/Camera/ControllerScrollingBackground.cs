using UnityEngine;
using Commons.SideScrolling;

public class ControllerScrollingBackground : MonoBehaviour
{
    private ScrollBackground scroll;

    private void Awake()
    {
        scroll = GetComponentInChildren<ScrollBackground>();
    }

    private void Start()
    {
        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        ControllerMainMenu.Instance.GameEnded += OnGameEnded;
        //ControllerGame.Instance.GamePaused += OnGamePaued;
        //ControllerGame.Instance.GameResumed += OnGameResumed;

        scroll.Disable();
    }

    private void OnGameResumed()
    {
        scroll.Resume();
    }

    private void OnGamePaued()
    {
        scroll.Pause();
    }

    private void OnGameEnded()
    {
        scroll.Disable();
    }

    private void OnGameStarted()
    {
        scroll.Enable();
    }
}
