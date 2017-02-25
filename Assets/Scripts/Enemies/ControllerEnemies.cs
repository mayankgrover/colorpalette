using Commons.Singleton;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class ControllerEnemies : MonoSingleton<ControllerEnemies>
{
    private float nextPosition;

    private List<ColorStrip> strips = new List<ColorStrip>();
    private List<ChildColorStrip> childStrips = new List<ChildColorStrip>();

    public Action ClearedLevel;

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += GameStarted;
        ControllerMainMenu.Instance.GameEnded += GameEnded;
    }

    private void GameStarted()
    {
        nextPosition = strips.Count + childStrips.Count;
    }

    private void GameEnded()
    {
        MoveStrips(-transform.position.y);
        ResetStrips();
    }

    public void AddStrip(ColorStrip strip)
    {
        if (!strips.Contains(strip)) {
            strips.Add(strip);
        }
    }

    public void AddChildStrip(ChildColorStrip strip)
    {
        if(!childStrips.Contains(strip)) {
            childStrips.Add(strip);
        }

    }

    void Update() {
        CheckAndMoveIfAllStripsCrossed();
    }

    private void CheckAndMoveIfAllStripsCrossed()
    {
        if((strips.Count > 0 &&
            strips.Where(strip => strip.isCrossedByPlayer == false).Count() == 0) && 
           (childStrips.Count > 0 &&
           childStrips.Where(strip => strip.isCrossedByPlayer == false).Count() == 0))
        {
            //Debug.Log("Level cleared");
            if (ClearedLevel != null) ClearedLevel();
            MoveStrips(nextPosition);
            ResetStrips();
        }
    }

    private void MoveStrips(float nextPosition)
    {
        Vector3 pos = gameObject.transform.position;
        pos.y += nextPosition;
        gameObject.transform.position = pos;
    }

    private void ResetStrips()
    {
        //Debug.Log("Colors to use: " + ControllerGame.Instance.ColorsToUse);
        strips.ForEach(strip => strip.ResetStrip());
    }
}
