using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private static Color[] colors = { Color.red, Color.blue, Color.yellow };

    new Rigidbody2D rigidbody;
    int myColorIndex = 0;
    SpriteRenderer sprite;

    float playerSpeed = 0.5f;
    float playerSpeedInc = 0.1f;
    Vector3 startPos;

	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        startPos = transform.position;

        ControllerMainMenu.Instance.GameStarted += StartGame;
        ControllerMainMenu.Instance.GameEnded += EndGame;
        ControllerEnemies.Instance.ClearedLevel += ClearedLevel;
	}

    void Update () {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended) {
                ChangeColor();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            ChangeColor();
        }
	}

    private void ClearedLevel()
    {
        Vector3 currSpeed = rigidbody.velocity;
        currSpeed.y += playerSpeedInc;
        rigidbody.velocity = currSpeed;
    }

    private void StartGame()
    {
        transform.position = startPos;
        rigidbody.velocity = Vector3.up * playerSpeed;
        myColorIndex = 0;
        UpdateColor();
    }

    private void EndGame()
    {
        //transform.position = startPos;
        rigidbody.velocity = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("ColorStrip")) {
            ColorStrip strip = collider.gameObject.GetComponent<ColorStrip>();
            if (strip.myColor == colors[myColorIndex]) {
                ControllerScore.Instance.AddScore();
            } else {
                ControllerMainMenu.Instance.EndGame();
            }
        }
    }
        

    private void ChangeColor() {
        myColorIndex = (myColorIndex + 1) % colors.Length;
        UpdateColor();
    }

    private void UpdateColor() {
        //Debug.Log("Setting color to: " + myColorIndex);
        sprite.color = colors[myColorIndex];
    }
}
