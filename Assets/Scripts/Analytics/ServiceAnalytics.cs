using Commons.Singleton;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ServiceAnalytics : MonoSingleton<ServiceAnalytics>
{
    protected override void Start()
    {
        base.Start();
        Debug.Log("[Analytic-UserId] ID: " + SystemInfo.deviceUniqueIdentifier);
        Analytics.SetUserId(SystemInfo.deviceUniqueIdentifier);
    }

    public void ReportClickWatchAdToRevive(bool status)
    {
        Dictionary<string, object> param = new Dictionary<string, object> {
                { "revive_player", status },
                { "waves_cleared", ControllerEnemies.Instance.WavesCleared },
                { "stars_collected", ControllerEnemies.Instance.StarsCollected },
                { "stars_spawned", ControllerEnemies.Instance.StarsSpawned },
                { "death_wave", ControllerEnemies.Instance.DeathWave },
                { "death_strip", ControllerEnemies.Instance.DeathStrip }
        };
        Debug.Log("[Analytic-WatchAd] " + DebugPrint(param));
        Analytics.CustomEvent(StringConstants.ANALYTICS_REVIVE_PLAYER, param);
    }

    public void ReportPlayerDied()
    {
        Dictionary<string, object> param = new Dictionary<string, object> {
                { "score", ControllerScore.Instance.currentScore },
                { "waves_cleared", ControllerEnemies.Instance.WavesCleared },
                { "stars_collected", ControllerEnemies.Instance.StarsCollected },
                { "stars_spawned", ControllerEnemies.Instance.StarsSpawned },
                { "death_wave", ControllerEnemies.Instance.DeathWave },
                { "death_strip", ControllerEnemies.Instance.DeathStrip }
        };
        Debug.Log("[Analytic-PlayerDied] " + DebugPrint(param));
        Analytics.CustomEvent(StringConstants.ANALYTICS_GAME_OVER, param);
    }

    public void ReportClaimFreeReward(bool status)
    {
        Debug.Log("[Analutic-FreeReward] claim: " + status);
        Analytics.CustomEvent(StringConstants.ANALYTICS_CLAIM_FREE_REWARD, 
            new Dictionary<string, object> {
                { "claim_free_reward", status }
        });
    }

    private string DebugPrint(Dictionary<string, object> dict)
    {
        string msg = "";
        foreach(KeyValuePair<string, object> pair in dict) {
            msg += "{" + pair.Key + ":" + pair.Value + "}";
        }
        return msg;
    }
}
