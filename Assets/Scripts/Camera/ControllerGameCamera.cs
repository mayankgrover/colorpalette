
using System;
using UnityEngine;

public class ControllerGameCamera: MonoBehaviour
{
    private void Start()
    {
        ControllerMainMenu.Instance.GameStarted += onGameStarted;
        ControllerMainMenu.Instance.GameEnded += onGameEnded;

        gameObject.SetActive(false);
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
