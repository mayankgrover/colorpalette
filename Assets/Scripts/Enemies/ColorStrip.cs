﻿using Commons.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Strip
{
    Strip1, 
    Strip2,
    Strip3,
    Strip4,
    Strip5,
    Strip6,
    Strip7,
    Strip8,
    Strip9,
    Strip10,

    ChildStrip1,
    ChildStrip2,
    ChildStrip3,
    ChildStrip4,
    ChildStrip5,
    ChildStrip6,
    ChildStrip7,
    ChildStrip8,
    ChildStrip9,
    ChildStrip10,
    None,
}

public class ColorStrip : MonoBehaviour {

    [SerializeField] public Strip strip;

    public bool isCrossedByPlayer { get; protected set; }
    public Color myColor { get; private set; }

    protected ControllerEnemiesGroup enemyGroup;
    protected List<ChildColorStrip> childStrips;

    private SpriteRenderer sprite;
    private int myColorIndex;
    private iTween tween;

    protected GameObject stripView;

    public bool IsSpriteEnabled { get { return stripView.transform.localScale != Vector3.zero; } }

    protected virtual void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        enemyGroup = GetComponentInParent<ControllerEnemiesGroup>();
        childStrips = GetComponentsInChildren<ChildColorStrip>().ToList();
        SetupStripView();
        RegisterEvents();
    }

    private void SetupStripView()
    {
        stripView = new GameObject();
        stripView.transform.SetParent(transform);
        stripView.transform.localPosition = Vector3.zero;
        stripView.transform.localScale = Vector3.one;
        stripView.layer = gameObject.layer;

        SpriteRenderer childSprite = stripView.AddComponent<SpriteRenderer>();
        childSprite.sprite = sprite.sprite;
        childSprite.color = sprite.color;
        childSprite.material = sprite.material;
        childSprite.sortingLayerID = sprite.sortingLayerID;
        childSprite.sortingOrder = sprite.sortingOrder;
        sprite.enabled = false;
        sprite = childSprite;
    }

    public void CancelTweens()
    {
        TweenUtil.CancelPendingTweens(stripView);
    }

    protected virtual void RegisterEvents() {
        enemyGroup.AddStrip(this);
    }

    public virtual void ResetStrip() {
        //Debug.Log("[ResetStrip] group:" + enemyGroup.wave + " strip: " + strip, gameObject);
        CancelTweens();
        isCrossedByPlayer = false;
        stripView.transform.localScale = Vector3.one;  //defScale;
        ResetColor();
    }

    private void ShrinkStrip()
    {
        iTween.ScaleTo(stripView, Vector3.zero, 0.5f);
    }

    protected virtual void ResetColor(int colorIndex = -1)
    {
        colorIndex = colorIndex != -1 ? colorIndex : UnityEngine.Random.Range(0, 10000) % 
            ControllerGame.Instance.ColorsToUse;
        //Debug.Log("[Strip] group:" + enemyGroup.wave + " strip: " + strip + " color:" + colorIndex, gameObject);
        myColorIndex = colorIndex;
        myColor = Colors.Instance.GameColors[colorIndex];
        sprite.color = myColor;
        ResetChildColor();
    }

    internal void PlayerMatchedColor()
    {
        ShrinkStrip();
        isCrossedByPlayer = true;
    }

    private void ResetChildColor()
    {
        if(childStrips.Count > 0) {
            int colorIndex = myColorIndex + 1;
            for(int i = 0; i < childStrips.Count; i++) {
                int temp = (colorIndex) % (ControllerGame.Instance.ColorsToUse);
                //Debug.Log("mycolor: " + myColorIndex + ", giving child color:" + temp, childStrips[i].gameObject);
                childStrips[i].ResetColor((colorIndex) % (ControllerGame.Instance.ColorsToUse));
                colorIndex++;
            }
        }
    }
}
