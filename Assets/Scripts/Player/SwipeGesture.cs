using UnityEngine;
using Commons.Singleton;

public class SwipeGesture : MonoSingleton<SwipeGesture>
{
    private Vector2 startPosition;
    private float startTime;

    private int myPlayerPosition = 0;
    private float maxPosition = 0.4f;
    private float incPosition = 0.2f;

    void Update()
    {
        if (Input.touchCount > 0  && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Vector2 endPosition = Input.GetTouch(0).position;
            Vector2 delta = endPosition - startPosition;

            float dist = Mathf.Sqrt(Mathf.Pow(delta.x, 2) + Mathf.Pow(delta.y, 2));
            float angle = Mathf.Atan(delta.y / delta.x) * (180.0f / Mathf.PI);
            float duration = Time.time - startTime;
            float speed = dist / duration;

            if (angle < 0) angle = angle * -1.0f;

            if (dist > 20 && angle < 40 && speed > 100) {
                // Left to Right swipe
                if (startPosition.x < endPosition.x) {
                    //Debug.Log("[LtR] Distance: " + dist + " Angle: " + angle + " Speed: " + speed);
                    SwipeRight();
                }
                // Right to Left swipe
                else if (startPosition.x > endPosition.x) {
                    //Debug.Log("[RtL] Distance: " + dist + " Angle: " + angle + " Speed: " + speed);
                    SwipeLeft();
                }
            }
            else Debug.Log("[NoSwipe] Distance: " + dist + " Angle: " + angle + " Speed: " + speed);
        }

        if (Input.touchCount > 0  && Input.GetTouch(0).phase == TouchPhase.Began) {
            startPosition = Input.GetTouch(0).position;
            startTime = Time.time;
        }
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