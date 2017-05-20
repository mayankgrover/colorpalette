
using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ControllerGameCamera: MonoBehaviour
{
    private Camera gameCamera;

    private void Awake()
    {
        gameCamera = GetComponent<Camera>();
        gameCamera.enabled = false;
    }

    private void Start()
    {
        ControllerMainMenu.Instance.GameStarted += onGameStarted;
        ControllerMainMenu.Instance.GameEnded += onGameEnded;

        ControllerGame.Instance.GamePaused += onGamePaused;
        ControllerGame.Instance.GameResumed += onGameResumed;
    }

    private void onGameResumed()
    {
        Debug.Log("[GameCam] Resume - enabled");
        gameCamera.enabled = true;
    }

    private void onGamePaused()
    {
        Debug.Log("[GameCam] Paused - disabled");
        gameCamera.enabled = false;
    }

    private void onGameStarted()
    {
        Debug.Log("[GameCam] Start - enabled");
        gameCamera.enabled = true;
    }

    private void onGameEnded()
    {
        Debug.Log("[GameCam] End - disabled");
        gameCamera.enabled = false;
    }
}
