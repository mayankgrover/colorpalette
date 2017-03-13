
using Commons.Singleton;
using System;
using UnityEngine;

public class PlayerProfile : MonoSingleton<PlayerProfile>
{
    private float best;
    private int coins;
    private int deaths;
    private long freeGiftTicks;

    public Action OnBestScoreUpdated;
    public Action OnCoinsUpdated;
    public Action OnDeathsUpdated;

    public override void OnInitialized()
    {
        base.OnInitialized();
        ControllerMainMenu.Instance.GameEnded += IncrementDeaths;
    }

    public long FreeGiftTick {
        get { return freeGiftTicks; }
    }

    public float BestScore {
        get { return best; }
    }

    public int Coins {
        get { return coins; }
    }

    public int Deaths {
        get { return deaths; }
    }

    protected override void Awake()
    {
        base.Awake();
        LoadPlayerProfile();
    }

    private void LoadPlayerProfile()
    {
        best = PlayerPrefs.GetFloat(StringConstants.BEST_SCORE, 0f);
        coins = PlayerPrefs.GetInt(StringConstants.COINS, 0);
        deaths = PlayerPrefs.GetInt(StringConstants.DEATHS, 0);
        freeGiftTicks = long.Parse(PlayerPrefs.GetString(StringConstants.FREE_GIFT_TICKS, "" + DateTime.MinValue.Ticks));
    }

    public void UpdateBestScore(float score)
    {
        if(score > best) {
            best = score;
            SaveBestScore();
            if (OnBestScoreUpdated != null) OnBestScoreUpdated();
        }
    }

    public void UpdateCoins(int coins)
    {
        this.coins += coins;
        SaveCoins();
        if (OnCoinsUpdated != null) OnCoinsUpdated();
    }

    public void UpdateFreeGiftTimestamp(long ticks)
    {
        freeGiftTicks = ticks;
        PlayerPrefs.SetString(StringConstants.FREE_GIFT_TICKS, ticks.ToString());
        PlayerPrefs.Save();
    }

    public void IncrementDeaths()
    {
        deaths++;
        SaveDeaths();
        if (OnDeathsUpdated != null) OnDeathsUpdated();
    }

    private void SaveDeaths()
    {
        PlayerPrefs.SetInt(StringConstants.DEATHS, deaths);
        PlayerPrefs.Save();
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(StringConstants.COINS, coins);
        PlayerPrefs.Save();
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetFloat(StringConstants.BEST_SCORE, best);
        PlayerPrefs.Save();
    }
}