using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorStrip : MonoBehaviour {

    public bool isCrossedByPlayer { get; protected set; }
    public Color myColor { get; private set; }

    protected ControllerEnemiesGroup enemyGroup;
    protected List<ChildColorStrip> childStrips;

    private SpriteRenderer sprite;
    private int myColorIndex;
    private ObstacleBlock obstacle;

    protected virtual void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        obstacle = transform.GetChild(0).GetComponent<ObstacleBlock>();
        enemyGroup = GetComponentInParent<ControllerEnemiesGroup>();
        childStrips = GetComponentsInChildren<ChildColorStrip>().ToList();
        RegisterEvents();
    }

    protected virtual void RegisterEvents() {
        enemyGroup.AddStrip(this);
    }

    public virtual void ResetStrip() {
        isCrossedByPlayer = false;
        ResetColor();
        SetObstacle(false);
    }

    public void SetObstacle(bool status)
    {
        if(obstacle != null) {
            obstacle.SetStatus(status);
        }
    }

    protected virtual void ResetColor(int colorIndex = -1)
    {
        colorIndex = colorIndex != -1 ? colorIndex : Random.Range(0, 10000) % ControllerGame.Instance.ColorsToUse;
        myColorIndex = colorIndex;
        myColor = Colors.GameColors[colorIndex];
        sprite.color = myColor;
        ResetChildColor();
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player")) {
            isCrossedByPlayer = true;
        }
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
