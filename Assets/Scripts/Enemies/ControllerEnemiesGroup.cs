
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Wave
{
    Tutorial, // Tutorial
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
    public int StarsSpawned { get; private set; }

    private List<ColorStrip> strips = new List<ColorStrip>();
    private List<ChildColorStrip> childStrips = new List<ChildColorStrip>();
    private List<ObstacleBlock> obstacles = new List<ObstacleBlock>();

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

    public void AddObstacle(ObstacleBlock obstacle)
    {
        if(obstacles.Contains(obstacle) == false) {
            obstacles.Add(obstacle);
        }
    }

    public void AddStrip(ColorStrip strip)
    {
        if(strips.Contains(strip) == false) {
            strips.Add(strip);
        }
    }

    public void AddChildStrip(ChildColorStrip childStrip)
    {
        if(childStrips.Contains(childStrip) == false) {
            childStrips.Add(childStrip);
        }
    }

    public void ResetStrips()
    {
        strips.ForEach(strip => strip.ResetStrip());
        SpawnStars();
    }

    private void SpawnStars()
    {
        //obstacles.ForEach(obstacle => obstacle.SetStatus(false));
        obstacles.ForEach(obstacle => obstacle.SetStatus(true));

        //int firstStar  = GetRandomStarIndex();
        //int secondStar = GetRandomStarIndex();
        //while (secondStar == firstStar) secondStar = GetRandomStarIndex();

        //obstacles[firstStar].SetStatus(true);
        //obstacles[secondStar].SetStatus(true);
        //StarsSpawned = 2;
    }

    private int GetRandomStarIndex() {
        return UnityEngine.Random.Range(0, strips.Count);
    }

    public void Show() {
        //Debug.Log("Showing group:" + wave);
        gameObject.SetActive(true);
    }

    public void Hide() {
        //Debug.Log("Hiding group:" + wave);
        gameObject.SetActive(false);
    }
}
