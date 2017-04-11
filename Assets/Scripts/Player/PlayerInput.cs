using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput: MonoBehaviour, IPointerClickHandler//, IDragHandler, IEndDragHandler
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
        Debug.Log("[PlayerInput] OnPointerClick dragging:" + eventData.dragging +
            " eligibleClick:" + eventData.eligibleForClick);
        if (!ControllerGame.Instance.IsGamePaused && !eventData.dragging) {
            player.ChangeColor();
        }
    }

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Debug.Log("[PlayerInput] OnEndDrag dragging:" + eventData.dragging +
    //        " eligibleClick:" + eventData.eligibleForClick);
    //    if (!ControllerGame.Instance.IsGamePaused) {
    //    }
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    Debug.Log("[PlayerInput] OnDrag dragging:" + eventData.dragging +
    //        " eligibleClick:" + eventData.eligibleForClick);
    //    if (!ControllerGame.Instance.IsGamePaused) {
    //    }
    //}
}
