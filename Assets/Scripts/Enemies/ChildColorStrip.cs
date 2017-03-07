
using UnityEngine;

public class ChildColorStrip: ColorStrip
{
    protected override void Awake() {
        base.Awake();
        childStrips.Remove(this);
    }

    protected override void RegisterEvents() {
        enemyGroup.AddChildStrip(this);
    }

    protected override void ResetColor(int colorIndex = -1)
    {
        //Debug.Log("setting child color:" + colorIndex, gameObject);
        base.ResetColor(colorIndex);
        isCrossedByPlayer = false;
        ResetObstacle();
    }
}
