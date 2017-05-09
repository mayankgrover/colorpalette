
using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ControllerGameCamera: MonoBehaviour
{
    private Camera gameCamera;

    private void Awake()
    {
        gameCamera = GetComponent<Camera>();
    }

    private void Start()
    {
        ControllerMainMenu.Instance.GameStarted += onGameStarted;
        ControllerMainMenu.Instance.GameEnded += onGameEnded;

        ControllerGame.Instance.GamePaused += onGamePaused;
        ControllerGame.Instance.GameResumed += onGameResumed;

        gameObject.SetActive(false);
    }

    private void onGameResumed()
    {
        gameCamera.enabled = true;
    }

    private void onGamePaused()
    {
        gameCamera.enabled = false;
    }

    private void onGameStarted()
    {
        gameObject.SetActive(true);
    }

    private void onGameEnded()
    {
        gameObject.SetActive(false);
    }
}
