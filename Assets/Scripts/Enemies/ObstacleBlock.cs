
using Commons.Services;
using UnityEngine;

public class ObstacleBlock: MonoBehaviour
{
    private SpriteRenderer sprite;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            ControllerEnemies.Instance.StarCollected();
            PlayerProfile.Instance.UpdateCoins(NumericConstants.COINS_FOR_STAR);
            //ControllerScore.Instance.AddScore(NumericConstants.COINS_FOR_STAR);
            ControllerScore.Instance.BonusScore(gameObject.transform.position, NumericConstants.COINS_FOR_STAR);
            ServiceSounds.Instance.PlaySoundEffect(SoundEffect.Game_Bonus);
            gameObject.SetActive(false);
        }
    }

    public void SetStatus(bool status)
    {
        gameObject.SetActive(status);
    }
}
