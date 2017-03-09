
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerEnemiesGroup: MonoBehaviour
{
    [SerializeField]
    public float GroupLength;

    public bool IsGroupCleared { get { return CheckIfAllStripsCrossed(); } }
    public int StripCount { get { return strips.Count + childStrips.Count; } }

    private List<ColorStrip> strips = new List<ColorStrip>();
    private List<ChildColorStrip> childStrips = new List<ChildColorStrip>();
    private float showStarProbability   = 0.30f; 

    private bool CheckIfAllStripsCrossed()
    {
        if( (strips.Count == 0 || strips.Where(strip => strip.isCrossedByPlayer == false).Count() == 0) &&
            (childStrips.Count == 0 || childStrips.Where(strip => strip.isCrossedByPlayer == false).Count() == 0)) {
            return true;
        }

        return false;
    }

    public void Initialize(Vector3 position, Transform parent)
    {
        transform.SetParent(parent);
        transform.position = position;
    }

    public void AddStrip(ColorStrip strip)
    {
        if(!strips.Contains(strip)) {
            strips.Add(strip);
        }
    }

    public void AddChildStrip(ChildColorStrip childStrip)
    {
        if(!childStrips.Contains(childStrip)) {
            childStrips.Add(childStrip);
        }
    }

    public void ResetStrips()
    {
        strips.ForEach(strip => strip.ResetStrip());
        float rand = Random.value;
        Debug.Log("[Star] rand: " + rand + " prob: " + showStarProbability);
        if(rand < showStarProbability) {
            strips[Random.Range(0, strips.Count)].SetObstacle(true);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
