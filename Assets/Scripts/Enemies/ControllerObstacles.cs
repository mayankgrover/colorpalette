using Commons.Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Star,
    ButterFly,
    Ant,
}

[Serializable]
public struct Obstacle
{
    [SerializeField] public ObstacleType type;
    [SerializeField] public Sprite sprite;
}

public class ControllerObstacles: MonoSingleton<ControllerObstacles>
{
    [SerializeField] private List<Obstacle> obstacles;

    public Sprite GetObstacleSprite(ObstacleType type)
    {
        return obstacles.Find(o => o.type == type).sprite;
    }

    public ObstacleType GetRandomObstacleType()
    {
        return ObstacleType.Star;
        //return (ObstacleType) UnityEngine.Random.Range(0, Enum.GetValues(typeof(ObstacleType)).Length);
    }

    public int GetObstacleReward(ObstacleType type)
    {
        switch(type)
        {
            case ObstacleType.Star: return 1;
            case ObstacleType.ButterFly: return 2;
            case ObstacleType.Ant: return 3;
        }

        return 0;
    }
}
