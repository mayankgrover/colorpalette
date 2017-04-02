using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput: MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlayerController player;

    private void Start()
    {
        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        ControllerMainMenu.Instance.GameEnded += OnGameEnded;

        gameObject.SetActive(false);
    }

    private void OnGameEnded()
    {
        gameObject.SetActive(false);
    }

    private void OnGameStarted()
    {
        gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!ControllerGame.Instance.IsGamePaused) {
            player.ChangeColor();
        }
    }
}
