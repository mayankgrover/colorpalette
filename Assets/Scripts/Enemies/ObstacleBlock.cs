
using System;
using Commons.Services;
using UnityEngine;
using System.Collections;

public class ObstacleBlock: MonoBehaviour
{
    private ControllerEnemiesGroup enemyGroup;
    private SpriteRenderer sprite;
    private ObstacleType currentType;
    private float pulsatingSpeed;
    private bool isTweening = false;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        enemyGroup = GetComponentInParent<ControllerEnemiesGroup>();
        enemyGroup.AddObstacle(this);

        iTween.Init(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            int reward = ControllerObstacles.Instance.GetObstacleReward(currentType);
            ControllerEnemies.Instance.StarCollected();
            PlayerProfile.Instance.UpdateCoins(reward);
            ControllerScore.Instance.BonusScore(gameObject.transform.position, reward);
            ServiceSounds.Instance.PlaySoundEffect(SoundEffect.Game_Bonus);
            gameObject.SetActive(false);
        }
    }

    public void SetStatus(bool status)
    {
        if (status == true) RandomizeObstacle();
        gameObject.SetActive(status);
    }

    private void RandomizeObstacle()
    {
        currentType = ControllerObstacles.Instance.GetRandomObstacleType();
        sprite.sprite = ControllerObstacles.Instance.GetObstacleSprite(currentType);
        StartTween();
    }

    private void StartTween()
    {
        if(isTweening == false)
        {
            isTweening = true;
            iTween.ScaleTo(gameObject, new Hashtable() {
                { "x", transform.localScale.x * 1.25f },
                { "y", transform.localScale.y * 1.25f },
                { "time", 1f },
                { "easetype", iTween.EaseType.linear },
                { "looptype", iTween.LoopType.pingPong },
            });
        }
    }
}
