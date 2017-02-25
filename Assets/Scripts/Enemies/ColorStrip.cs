using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorStrip : MonoBehaviour {

    public bool isCrossedByPlayer { get; protected set; }
    public Color myColor { get; private set; }

    private SpriteRenderer sprite;
    private int myColorIndex;
    private List<ChildColorStrip> childStrips = new List<ChildColorStrip>();
    private ObstacleBlock obstacle;

    void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        obstacle = transform.GetChild(0).GetComponent<ObstacleBlock>();
    }

	protected virtual void Start () {
        childStrips = GetComponentsInChildren<ChildColorStrip>().ToList();
        ControllerEnemies.Instance.AddStrip(this);
        RegisterEvents();
	}

    protected virtual void RegisterEvents() {
        ControllerMainMenu.Instance.GameStarted += ResetStrip;
    }

    public void ResetStrip() {
        isCrossedByPlayer = false;
        ResetColor();
    }

    protected virtual void ResetObstacle()
    {
        if(obstacle != null) {
            obstacle.Reset();
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
