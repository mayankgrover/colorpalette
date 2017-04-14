
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Wave
{
    None,
    Wave1,
    Wave2,
    Wave3,
    Wave4,
    Wave5,
    Wave6,
    Wave7,
    Wave8,
    Wave9,
    Wave10,
}

public class ControllerEnemiesGroup: MonoBehaviour
{
    [SerializeField] public float GroupLength;
    [SerializeField] public Wave wave;

    public bool IsGroupCleared { get { return CheckIfAllStripsCrossed(); } }
    public int StripCount { get { return strips.Count + childStrips.Count; } }

    private List<ColorStrip> strips = new List<ColorStrip>();
    private List<ChildColorStrip> childStrips = new List<ChildColorStrip>();

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
        //if(rand < NumericConstants.SHOW_STAR_PROBABILITY)
        {
            strips[Random.Range(0, strips.Count)].SetObstacle(true);
            strips[Random.Range(0, strips.Count)].SetObstacle(true);
            if (childStrips.Count > 0) {
                childStrips[Random.Range(0, childStrips.Count)].SetObstacle(true);
            }
        }
    }

    public void Show()
    {
        Debug.Log("Showing group:" + wave);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Debug.Log("Hiding group:" + wave);
        gameObject.SetActive(false);
    }
}
