
using Commons.Singleton;
using System;
using UnityEngine;

public class PlayerProfile : MonoSingleton<PlayerProfile>
{
    private float best;
    private int coins;
    private int deaths;

    public Action OnBestScoreUpdated;
    public Action OnCoinsUpdated;
    public Action OnDeathsUpdated;

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
        //if(this.coins != coins)
        {
            this.coins = coins;
            SaveCoins();
            if (OnCoinsUpdated != null) OnCoinsUpdated();
        }
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
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(StringConstants.COINS, coins);
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetFloat(StringConstants.BEST_SCORE, best);
    }
}