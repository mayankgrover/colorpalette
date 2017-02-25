
using UnityEngine;

public class ChildColorStrip: ColorStrip
{
    protected override void Start()
    {
        ControllerEnemies.Instance.AddChildStrip(this);
        RegisterEvents();
    }

    protected override void RegisterEvents()
    {
        ControllerMainMenu.Instance.GameStarted += () => {
            isCrossedByPlayer = false;
        };
    }

    protected override void ResetColor(int colorIndex = -1)
    {
        //Debug.Log("setting child color:" + colorIndex, gameObject);
        base.ResetColor(colorIndex);
        isCrossedByPlayer = false;
        ResetObstacle();
    }
}
