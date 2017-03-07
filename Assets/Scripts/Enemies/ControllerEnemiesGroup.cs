
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerEnemiesGroup: MonoBehaviour
{
    [SerializeField]
    public int GroupLength;

    public bool IsGroupCleared { get { return CheckIfAllStripsCrossed(); } }
    public int StripCount { get { return strips.Count + childStrips.Count; } }

    List<ColorStrip> strips = new List<ColorStrip>();
    List<ChildColorStrip> childStrips = new List<ChildColorStrip>();

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
