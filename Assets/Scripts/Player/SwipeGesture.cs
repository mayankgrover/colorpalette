using Commons.Services;
using System.Collections;
using UnityEngine;
using System;

public class SwipeGesture : MonoBehaviour
{
    private Vector2 startPosition;
    private float startTime;
    private PlayerController player;

    private float maxPosition = 0.3f;
    private float incPosition = 0.3f;

    private bool ignoreTouch = true;
    private Hashtable tweenOptions;
    private float lastTweenPosX;

    [SerializeField] private iTween.EaseType easeType;

    void Awake()
    {
        player = GetComponent<PlayerController>();

        tweenOptions = new Hashtable();
        tweenOptions["space"] = Space.World;
        tweenOptions["time"] = 0.15f;
        tweenOptions["easetype"] = easeType;

        iTween.Init(gameObject);
        lastTweenPosX = transform.position.x;
    }

    private void Start()
    {
        ControllerMainMenu.Instance.GameEnded += OnGameEnded;
    }

    private void OnGameEnded()
    {
        lastTweenPosX = 0f;
    }

    void Update()
    {
        if (!ControllerGame.Instance.IsGamePaused)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !ignoreTouch)
            {
                Vector2 endPosition = Input.GetTouch(0).position;
                Vector2 delta = endPosition - startPosition;

                float dist = Mathf.Sqrt(Mathf.Pow(delta.x, 2) + Mathf.Pow(delta.y, 2));
                float angle = Mathf.Atan(delta.y / delta.x) * (180.0f / Mathf.PI);
                float duration = Time.time - startTime;
                float speed = dist / duration;

                if (angle < 0) angle = angle * -1.0f;

                if (dist > 20 && angle < 40 && speed > 100)
                {
                    // Left to Right swipe
                    if (startPosition.x < endPosition.x)
                    {
                        Debug.Log("[LtR] Distance: " + dist + " Angle: " + angle + " Speed: " + speed);
                        SwipeRight();
                    }
                    // Right to Left swipe
                    else if (startPosition.x > endPosition.x)
                    {
                        Debug.Log("[RtL] Distance: " + dist + " Angle: " + angle + " Speed: " + speed);
                        SwipeLeft();
                    }
                }
                else
                {
                    Debug.Log("[NoSwipe] Distance: " + dist + " Angle: " + angle + " Speed: " + speed);
                    player.ChangeColor();
                }
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startPosition = Input.GetTouch(0).position;
                startTime = Time.time;
                ignoreTouch = true;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                ignoreTouch = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SwipeLeft();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SwipeRight();
            }
        }
    }

    private void SwipeLeft()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.Game_PlayerSwipe);
        MovePlayer(-1 * incPosition);
    }

    private void SwipeRight()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.Game_PlayerSwipe);
        MovePlayer(incPosition);
    }

    private void MovePlayer(float change)
    {
        //if(Mathf.Abs(transform.position.x - lastTweenPosX) > 0.1f) {
        //    Debug.Log("Forcing X:" + lastTweenPosX + " pos:" + transform.position.x);
        //    Vector3 prevPos = transform.position;
        //    prevPos.x = lastTweenPosX;
        //    transform.position = prevPos;
        //    CancelTween();
        //}

        CancelTween();
        transform.position = new Vector3(lastTweenPosX, transform.position.y, transform.position.z);

        Vector3 pos = transform.position;
        pos.x += change;
        if (Mathf.Abs(pos.x) <= maxPosition)
        {
            //Debug.Log("Updating X:" + pos.x + " curr:" + transform.position.x);
            lastTweenPosX = pos.x;
            tweenOptions["x"] = change;
            iTween.MoveBy(gameObject, tweenOptions);
        }
    }

    private void CancelTween()
    {
        iTween tween = transform.GetComponent<iTween>();
        if (tween != null) {
            //Debug.Log("killing existing tween while swiping");
            Destroy(tween);
        }
    }
}