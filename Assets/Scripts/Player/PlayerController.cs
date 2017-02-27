﻿using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    new Rigidbody2D rigidbody;
    int myColorIndex = 0;
    SpriteRenderer sprite;

    float defPlayerSpeed = 0.50f;
    float playerSpeedInc = 0.025f;
    Vector3 startPos;
    bool ignoreTouch = false;

	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        startPos = transform.position;

        ControllerMainMenu.Instance.GameStarted += StartGame;
        ControllerMainMenu.Instance.GameEnded += EndGame;
        ControllerEnemies.Instance.ClearedLevel += ResetPlayerSpeed;
	}

    void Update () {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved) {
                ignoreTouch = true;
            }
            else if (touch.phase == TouchPhase.Ended) {
                if(!ignoreTouch) ChangeColor();
                ignoreTouch = false;
            } 
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            ChangeColor();
        }
	}

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
        myColorIndex = UnityEngine.Random.Range(0, ControllerGame.Instance.ColorsToUse - 1);
        UpdateColor();
        ResetPlayerSpeed();
    }

    private void EndGame()
    {
        transform.position = startPos;
        rigidbody.velocity = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("ColorStrip")) {
            ColorStrip strip = collider.gameObject.GetComponent<ColorStrip>();
            if (strip.myColor == Colors.GameColors[myColorIndex]) {
                ControllerScore.Instance.AddScore();
            } else {
                //ControllerMainMenu.Instance.EndGame();
            }
        }
        //else if(collider.gameObject.CompareTag("ObstacleBlock")) {
        //    ControllerMainMenu.Instance.EndGame();
        //}
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("ColorStrip")) {
            IncreasePlayerSpeed();
        }
    }
        

    public void ChangeColor() {
        myColorIndex = (myColorIndex + 1) % ControllerGame.Instance.ColorsToUse;
        UpdateColor();
    }

    private void UpdateColor() {
        //Debug.Log("Setting color to: " + myColorIndex);
        sprite.color = Colors.GameColors[myColorIndex];
    }
}
