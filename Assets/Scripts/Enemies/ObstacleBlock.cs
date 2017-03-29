
using UnityEngine;

public class ObstacleBlock: MonoBehaviour
{
    private SpriteRenderer sprite;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            ControllerScore.Instance.AddScore(NumericConstants.COINS_FOR_STAR);
            ControllerEnemies.Instance.StarCollected();
            //PlayerProfile.Instance.UpdateCoins(NumericConstants.COINS_FOR_STAR);
            ControllerScore.Instance.BonusScore(gameObject.transform.position, NumericConstants.COINS_FOR_STAR);
            gameObject.SetActive(false);
        }
    }

    public void SetStatus(bool status)
    {
        gameObject.SetActive(status);
    }
}
