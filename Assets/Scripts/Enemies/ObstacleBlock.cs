﻿
using UnityEngine;

public class ObstacleBlock: MonoBehaviour
{
    //private float showProbability   = 0.1f; 
    //private float incShowProbablity = 0f;  
    //private float maxShowProbabilty = 0.35f;

    private SpriteRenderer sprite;

    //void Awake()
    //{
    //    sprite = GetComponent<SpriteRenderer>();
    //    ControllerEnemies.Instance.ClearedLevel += OnLevelCleared;
    //    ControllerMainMenu.Instance.GameStarted += OnGameStarted;
    //}

    //private void OnGameStarted()
    //{
    //    //Reset();
    //}

    //private void OnLevelCleared()
    //{
    //    showProbability += incShowProbablity;
    //    showProbability = Mathf.Min(showProbability, maxShowProbabilty);
    //}

    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if(collider.gameObject.CompareTag("Player")) {
    //        ControllerScore.Instance.AddScore(ControllerGame.Bonus_Score);
    //        ControllerScore.Instance.BonusScore(gameObject.transform.position);
    //        gameObject.SetActive(false);
    //    }
    //}

    //internal void Reset()
    //{
    //    float rand = UnityEngine.Random.value;
    //    if (rand <= showProbability) gameObject.SetActive(true);
    //    else gameObject.SetActive(false); 
    //}

    public void SetStatus(bool status)
    {
        gameObject.SetActive(status);
    }
}
