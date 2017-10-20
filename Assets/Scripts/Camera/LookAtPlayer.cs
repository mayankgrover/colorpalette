
using System;
using UnityEngine;

public class LookAtPlayer: MonoBehaviour
{
    public PlayerController myPlayer;

    private bool lookAtX = false;
    private bool lookAtY = true;

    private float offsetX = 0f;
    private float offsetY = 0f; // 0.65f;

    private void Awake()
    {
        iTween.Init(gameObject);
    }

    //private void Start()
    //{
    //    ControllerMainMenu.Instance.GameEnded += OnGameEnded;
    //}

    public void ShakeCamera()
    {
        iTween.ShakePosition(gameObject, new Vector3(0.05f, 0.05f, 0f), 0.25f);
    }

    void Update()
    {
        Vector3 pos = transform.position;
        if (lookAtX) pos.x = myPlayer.transform.position.x;
        if (lookAtY) pos.y = myPlayer.transform.position.y;

        pos.x += offsetX;
        pos.y += offsetY;

        transform.position = pos;
    }

}
