using Commons.Services;
using System;
using System.Collections;
using UnityEngine;

public class CircularColor : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteMask mask;

    [SerializeField] private Vector2 maskMaxSize;
    [SerializeField] private Vector2 maskMinSize;

    private float speed = 2.5f;

    private int myColorIndex;
    public Color myColor { get { return Colors.Instance.GameColors[myColorIndex]; } }

    private Hashtable tweenOptions = new Hashtable();

    private void Awake()
    {
        tweenOptions["x"] = maskMinSize.x;
        tweenOptions["y"] = maskMinSize.y;
        tweenOptions["easetype"] = iTween.EaseType.linear;
        tweenOptions["time"] = speed;
        tweenOptions["oncomplete"] = "onTweenComplete";
        tweenOptions["oncompletetarget"] = gameObject;
    }

    public void StartPlaying()
    {
        ResetSize();
        ResetColor();
        ResetAnimation();
    }

    public void StopPlaying()
    {
        ResetSize();
        StopAnimation();
    }

    private void ResetAnimation()
    {
        StopAnimation();
        StartAnimation();
    }

    private void StopAnimation()
    {
        //Debug.Log("stoping animation");
        iTween.Stop(mask.gameObject);
    }

    private void StartAnimation()
    {
        //Debug.Log("starting animation");
        iTween.ScaleTo(mask.gameObject, tweenOptions);
    }

    private void ResetSize()
    {
        mask.transform.localScale = maskMaxSize;
    }

    private void ResetColor(int colorIndex = -1)
    {
        myColorIndex = colorIndex != -1 ? colorIndex :
            UnityEngine.Random.Range(0, 1000) % ControllerGame.Instance.ColorsToUse;
        sprite.color = Colors.Instance.GameColors[myColorIndex];
    }

    private void onTweenComplete()
    {
        if(player.MyColor == myColor)
        {
            ServiceSounds.Instance.PlaySoundEffect(SoundEffect.Game_Success);
            ControllerScore.Instance.AddScore();
            StartPlaying();
        }
        else
        {
            ServiceSounds.Instance.PlaySoundEffect(SoundEffect.Game_Over);
            ControllerGame.Instance.PlayerDied();
        }
    }
}
