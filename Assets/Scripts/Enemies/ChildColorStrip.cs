
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
        base.ResetColor(colorIndex);
        isCrossedByPlayer = false;
        CancelTweens();
        stripView.transform.localScale = Vector3.one;
        Debug.Log("[ChildStrip] group:" + enemyGroup.wave + " strip: " + strip + " ChildColor:" + colorIndex, gameObject);
    }
}
