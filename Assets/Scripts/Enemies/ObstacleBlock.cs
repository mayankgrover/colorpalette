
using System;
using UnityEngine;

public class ObstacleBlock: MonoBehaviour
{
    private float showProbability   = 0.20f;
    private float incShowProbablity = 0.05f;
    private float maxShowProbabilty = 0.35f;

    private SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        ControllerEnemies.Instance.ClearedLevel += OnLevelCleared;
        ControllerMainMenu.Instance.GameStarted += OnGameStarted;
    }

    private void OnGameStarted()
    {
        Reset();
    }

    private void OnLevelCleared()
    {
        showProbability += incShowProbablity;
        showProbability = Mathf.Min(showProbability, maxShowProbabilty);
    }

    internal void Reset()
    {
        float rand = UnityEngine.Random.value;
        if (rand <= showProbability) gameObject.SetActive(true);
        else gameObject.SetActive(false); 
    }
}
