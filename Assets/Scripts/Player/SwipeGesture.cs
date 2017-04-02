using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeGesture : MonoBehaviour
{
    private Vector2 startPosition;
    private float startTime;
    private PlayerController player;

    private int myPlayerPosition = 0;
    private float maxPosition = 0.3f;
    private float incPosition = 0.3f;

    private bool ignoreTouch = true;

    void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    void OnBeginDrag(BaseEventData eventData)
    {
    }

    private void OnDrag(BaseEventData eventData)
    {
    }

    private void OnDragEnd(BaseEventData eventData)
    {
    }

    private void SwipeLeft()
    {
        MovePlayer(-1 * incPosition);
    }

    private void SwipeRight()
    {
        MovePlayer(incPosition);
    }

    private void MovePlayer(float change)
    {
        Vector2 pos = transform.position;
        pos.x += change;
        if (Mathf.Abs(pos.x) <= maxPosition) {
            transform.position = pos;
        }
    }

    private void ResetPlayer()
    {
        Vector2 pos = transform.position;
        pos.x = 0;
        transform.position = pos;
    }
}