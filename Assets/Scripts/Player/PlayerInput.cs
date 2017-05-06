using Commons.Services;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput: MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler //, IInitializePotentialDragHandler
{
    [SerializeField] private PlayerController player;

    private bool isDragging = false;

    private void Start()
    {
        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
        ControllerMainMenu.Instance.GameEnded += OnGameEnded;

        //gameObject.SetActive(false);
    }

    private void OnGameEnded()
    {
        isDragging = false;
        gameObject.SetActive(false);
    }

    private void OnGameStarted()
    {
        isDragging = false;
        gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!ControllerGame.Instance.IsGamePaused && isDragging == false) {
            //Debug.Log("[PlayerInput] OnPointerClick: " + eventData);
            player.ChangeColor();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!ControllerGame.Instance.IsGamePaused) {
            isDragging = false;
            //Debug.Log("[PlayerInput] OnEndDrag: " + eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!ControllerGame.Instance.IsGamePaused) {
            //Debug.Log("[PlayerInput] OnDrag: " + eventData);
            isDragging = true;
        }
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (!ControllerGame.Instance.IsGamePaused) {
            //Debug.Log("[PlayerInput] OnInitializePotentialDrag: " + eventData);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!ControllerGame.Instance.IsGamePaused) {
            //Debug.Log("[PlayerInput] OnBeginDrag: " + eventData);
            isDragging = true;
        }
    }
}
