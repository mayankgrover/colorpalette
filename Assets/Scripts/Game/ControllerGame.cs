using Commons.Singleton;
using UnityEngine;

public class ControllerGame: MonoSingleton<ControllerGame>
{
    public int ColorsToUse { get; private set; }

    private int minColorsToUse = 3;
    private int incColorsToUse = 1;

    public static int Bonus_Score = 3;

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += GameStarted;
        ControllerMainMenu.Instance.GameEnded += GameEnded;
        ControllerEnemies.Instance.ClearedLevel += ClearedLevel;

        ColorsToUse = minColorsToUse;
    }

    private void ClearedLevel()
    {
        ColorsToUse += incColorsToUse;
        ColorsToUse = System.Math.Min(ColorsToUse, Colors.GameColors.Length);
        Debug.Log("colors to use:" + ColorsToUse);
    }

    private void GameEnded()
    {
        ColorsToUse = minColorsToUse;
    }

    private void GameStarted()
    {
        Random.seed = (int)Time.realtimeSinceStartup;
        ColorsToUse = minColorsToUse;
    }
}
