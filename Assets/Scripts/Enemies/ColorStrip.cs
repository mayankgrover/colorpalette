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
    private ObstacleBlock obstacle;

    protected GameObject stripView;

    protected virtual void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        obstacle = transform.GetChild(0).GetComponent<ObstacleBlock>();
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

        SpriteRenderer childSprite = stripView.AddComponent<SpriteRenderer>();
        childSprite.sprite = sprite.sprite;
        childSprite.color = sprite.color;
        childSprite.material = sprite.material;
        childSprite.sortingLayerID = sprite.sortingLayerID;
        childSprite.sortingOrder = sprite.sortingOrder;
        sprite.enabled = false; //Destroy(sprite); 
        sprite = childSprite;
    }

    protected virtual void RegisterEvents() {
        enemyGroup.AddStrip(this);
    }

    public virtual void ResetStrip() {
        isCrossedByPlayer = false;
        stripView.transform.localScale = Vector3.one;
        ResetColor();
        SetObstacle(false);
    }

    public void ShrinkStrip()
    {
        iTween.ScaleTo(stripView, Vector3.zero, 0.5f);
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
