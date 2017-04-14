using Commons.Singleton;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ServiceAnalytics : MonoSingleton<ServiceAnalytics>
{
    public void ReportClickWatchAdToRevive(bool status)
    {
        Dictionary<string, object> param = new Dictionary<string, object> {
                { "revive_player", status },
                { "waves_cleared", ControllerEnemies.Instance.WavesCleared },
                { "stars_collected", ControllerEnemies.Instance.StarsCollected },
                { "stars_spawned", ControllerEnemies.Instance.StarsSpawned },
                { "death_wave", ControllerEnemies.Instance.DeathWave.ToString() },
                { "death_strip", ControllerEnemies.Instance.DeathStrip.ToString() }
        };
        AnalyticsResult result = Analytics.CustomEvent(StringConstants.ANALYTICS_REVIVE_PLAYER, param);
        //Debug.Log("[Analytic-WatchAd] " + DebugPrint(param));
        //Debug.Log("Result: " + result);
    }

    public void ReportPlayerDied()
    {
        Dictionary<string, object> param = new Dictionary<string, object> {
                { "score", GetScoreCategory(ControllerScore.Instance.currentScore) },
                { "waves_cleared", ControllerEnemies.Instance.WavesCleared },
                { "stars_collected", ControllerEnemies.Instance.StarsCollected },
                { "stars_spawned", ControllerEnemies.Instance.StarsSpawned },
                { "death_wave", ControllerEnemies.Instance.DeathWave.ToString() },
                { "death_strip", ControllerEnemies.Instance.DeathStrip.ToString() }
        };
        AnalyticsResult result = Analytics.CustomEvent(StringConstants.ANALYTICS_GAME_OVER, param);
        //Debug.Log("[Analytic-PlayerDied] " + DebugPrint(param));
        //Debug.Log("Result: " + result);
    }

    public void ReportClaimFreeReward(bool status)
    {
        Debug.Log("[Analutic-FreeReward] claim: " + status);
        Analytics.CustomEvent(StringConstants.ANALYTICS_CLAIM_FREE_REWARD, 
            new Dictionary<string, object> {
                { "claim_free_reward", status }
        });
    }

    public void ReportAutoWatchAds(bool status)
    {
        Analytics.CustomEvent(StringConstants.ANALYTICS_AUTO_WATCH_POPUP,
            new Dictionary<string, object> {
                { "auto_watch_ad_popup", status }
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

    private string GetScoreCategory(float score)
    {
        if (score <= 5) return "0-5";
        else if (score <= 10) return "6-10";
        else if (score <= 15) return "11-15";
        else if (score <= 25) return "16-25";
        else return "25+";
    }
}
