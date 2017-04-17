using Commons.Services;
using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    new Rigidbody2D rigidbody;
    int myColorIndex = 0;
    SpriteRenderer sprite;

    float defPlayerSpeed = 0.50f;
    float playerSpeedInc = 0.025f;
    float currPlayerSpeed = 0f;

    TrailRenderer trail;
    Vector3 startPos;
    Vector3 startScale;
    bool ignoreTouch = false;
    Vector3 levelClearedPos = Vector3.zero;

    private void Awake()
    {
        iTween.Init(gameObject);
        startPos = transform.position;
        startScale = transform.localScale;
    }

    private void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        trail = GetComponentInChildren<TrailRenderer>();

        ControllerMainMenu.Instance.GameStarted += StartGame;
        ControllerMainMenu.Instance.GameEnded += EndGame;
        ControllerEnemies.Instance.ClearedLevel += OnLevelCleared;
        ControllerEnemies.Instance.ForceClearedLevel += OnLevelForceCleared;

        ControllerGame.Instance.GamePaused += OnGamePaused;
        ControllerGame.Instance.GameResumed += OnGameResumed;
	}

    private void OnLevelCleared()
    {
        levelClearedPos = transform.position;
        ResetPlayerSpeed();
    }

    private void OnLevelForceCleared()
    {
        Vector2 newPosition = transform.position;
        newPosition.y = levelClearedPos.y;
        transform.position = newPosition;
        UnShrinkPlayer();
        ResetPlayerSpeed();
    }

#if UNITY_EDITOR
    void Update () {
        if (!ControllerGame.Instance.IsGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                ChangeColor();
            }
        }
	}
#endif

    private void ResetPlayerSpeed()
    {
        rigidbody.velocity = Vector3.up * defPlayerSpeed;
    }

    private void IncreasePlayerSpeed()
    {
        Vector3 currSpeed = rigidbody.velocity;
        currSpeed.y += playerSpeedInc;
        rigidbody.velocity = currSpeed;
    }

    private void StartGame()
    {
        transform.position = startPos;
        levelClearedPos = startPos;
        myColorIndex = UnityEngine.Random.Range(0, ControllerGame.Instance.ColorsToUse - 1);
        UnShrinkPlayer();
        UpdateColor();
        ResetPlayerSpeed();
    }

    private void EndGame()
    {
        transform.position = startPos;
        levelClearedPos = startPos;
        rigidbody.velocity = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("ColorStrip")) {
            //Debug.LogError("player hit strip");
            ColorStrip strip = collider.gameObject.GetComponent<ColorStrip>();
            if (strip.myColor == Colors.Instance.GameColors[myColorIndex])
            {
                strip.PlayerMatchedColor();
                ServiceSounds.Instance.PlaySoundEffect(SoundEffect.Game_Success);
                ControllerScore.Instance.AddScore();
            } else {
                ControllerEnemies.Instance.DeathStrip = strip.strip;
                StartCoroutine(DelayPlayerDeath());
            }
        }
    }

    private IEnumerator DelayPlayerDeath()
    {
        //yield return new WaitForSecondsRealtime(0.1f);
        ServiceSharing.Instance.CaptureScreenshotNow();
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.Game_Over);
        Handheld.Vibrate();
        rigidbody.velocity = Vector3.zero;
        yield return new WaitForSecondsRealtime(0.1f);
        ShrinkPlayer();
        yield return new WaitForSecondsRealtime(0.75f);
        ControllerGame.Instance.PlayerDied();
    }

    private void ShrinkPlayer()
    {
        iTween.ScaleTo(gameObject, Vector3.zero, 0.5f);
        rigidbody.velocity = Vector3.zero;
        if(trail != null) trail.enabled = false;
    }

    private void UnShrinkPlayer()
    {
        transform.localScale = Vector3.zero;
        iTween.ScaleTo(gameObject, startScale, 1f);
        if(trail != null) trail.enabled = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("ColorStrip")) {
            IncreasePlayerSpeed();
        }
    }
        
    public void ChangeColor() {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.Game_PlayerClick);
        myColorIndex = (myColorIndex + 1) % ControllerGame.Instance.ColorsToUse;
        UpdateColor();
    }

    private void UpdateColor() {
        //Debug.Log("[Player] Setting color to: " + myColorIndex);
        sprite.color = Colors.Instance.GameColors[myColorIndex];
    }

    private void OnGamePaused()
    {
        currPlayerSpeed = rigidbody.velocity.y;
        rigidbody.velocity = Vector3.zero;
    }

    private void OnGameResumed()
    {
        rigidbody.velocity = Vector3.up * currPlayerSpeed;
    }

}
