
using UnityEngine;

public class LookAtPlayer: MonoBehaviour
{
    public PlayerController myPlayer;

    private bool lookAtX = false;
    private bool lookAtY = true;

    private float offsetX = 0f;
    private float offsetY = 0.5f;

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
