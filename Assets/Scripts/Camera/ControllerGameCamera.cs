
using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ControllerGameCamera: MonoBehaviour
{
    [SerializeField] private CircularColor enemy;

    private Camera gameCamera;

    private void Awake()
    {
        gameCamera = GetComponent<Camera>();
        gameCamera.enabled = false;
        //enemy.enabled = false;
        enemy.gameObject.SetActive(false);
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
        enemy.gameObject.SetActive(true);
    }

    private void onGamePaused()
    {
        Debug.Log("[GameCam] Paused - disabled");
        gameCamera.enabled = false;
        enemy.gameObject.SetActive(false);
        //enemy.enabled = false;
    }

    private void onGameStarted()
    {
        Debug.Log("[GameCam] Start - enabled");
        gameCamera.enabled = true;
        enemy.gameObject.SetActive(true);
        enemy.StartPlaying();
        //enemy.enabled = true;
    }

    private void onGameEnded()
    {
        Debug.Log("[GameCam] End - disabled");
        gameCamera.enabled = false;
        enemy.gameObject.SetActive(false);
        enemy.StopPlaying();
        //enemy.enabled = false;
    }
}
