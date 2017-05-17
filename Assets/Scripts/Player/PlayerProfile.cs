
using Commons.Singleton;
using System;

using PlayerPrefs = ZPlayerPrefs;

public class PlayerProfile : MonoSingleton<PlayerProfile>
{
    private float currentScore;
    private int coinsEarned; //during the current game 
    private float best;
    private int coins;
    private int deaths;
    private int games;
    private long freeGiftTicks;
    private int autoWatchAds;
    private int characterId;
    private bool isTutorialCleared;

    public Action OnScoreUpdated;
    public Action OnBestScoreUpdated;
    public Action OnCoinsUpdated;
    public Action OnDeathsUpdated;
    public Action OnAutoWatchAdsUpdated;
    public Action OnSelectedCharacterUpdated;

    public override void OnInitialized()
    {
        base.OnInitialized();
        ControllerMainMenu.Instance.GameEnded += IncrementDeaths;
        ControllerMainMenu.Instance.GameStarted += IncrementGamesPlayed;
    }

    public int CoinsEarnedLastGame { get { return coinsEarned; } }
    public long GamesPlayed { get { return games; } }
    public long FreeGiftTick { get { return freeGiftTicks; } }
    public float BestScore { get { return best; } }
    public float CurrentScore { get { return currentScore; } }
    public int Coins { get { return coins; } }
    public int Deaths { get { return deaths; } }
    public int SelectedCharacterId { get { return characterId; } }
    public bool AutoWatchAds { get { return autoWatchAds == 1; } }

    public bool IsTutorialCleared { get {
            return isTutorialCleared;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        InitializePlayerPrefs();
        LoadPlayerProfile();
    }

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += onGameStarted;
        ControllerMainMenu.Instance.GameEnded += onGameEnded;
    }

    private void onGameEnded()
    {
        SaveCoins();
        SaveBestScore();
    }

    private void onGameStarted()
    {
        coinsEarned = 0;
    }

    private void InitializePlayerPrefs()
    {
        PlayerPrefs.Initialize("3YpIWkOfD!KtPqfyq", "Qe3IrH1sk");
    }

    private void LoadPlayerProfile()
    {
        best = PlayerPrefs.GetFloat(StringConstants.BEST_SCORE, 0f);
        coins = PlayerPrefs.GetInt(StringConstants.COINS, 0);
        deaths = PlayerPrefs.GetInt(StringConstants.DEATHS, 0);
        games = PlayerPrefs.GetInt(StringConstants.GAMES_PLAYED, 0);
        characterId = PlayerPrefs.GetInt(StringConstants.SELECTED_CHARACTER, 1);
        autoWatchAds = PlayerPrefs.GetInt(StringConstants.AUTO_WATCH_AD, 0);
        freeGiftTicks = long.Parse(PlayerPrefs.GetString(StringConstants.FREE_GIFT_TICKS, "" + DateTime.MinValue.Ticks));
        isTutorialCleared = PlayerPrefs.GetInt(StringConstants.TUTORIAL_STATUS, 0) == 0 ? false : true;
    }

    public void UpdateSelectedCharacter(int id)
    {
        characterId = id;
        PlayerPrefs.SetInt(StringConstants.SELECTED_CHARACTER, characterId);
        PlayerPrefs.Save();
        if (OnSelectedCharacterUpdated != null) OnSelectedCharacterUpdated();
    }

    public void UpdateAutoWatchAds(bool status)
    {
        autoWatchAds = status ? 1 : 0;
        PlayerPrefs.SetInt(StringConstants.AUTO_WATCH_AD, autoWatchAds);
        PlayerPrefs.Save();
        if (OnAutoWatchAdsUpdated != null) OnAutoWatchAdsUpdated();
    }

    public void UpdateScore(float score)
    {
        currentScore = score;
        if (OnScoreUpdated != null) OnScoreUpdated();
    }

    public void UpdateBestScore(float score)
    {
        if(score > best) {
            best = score;
            //SaveBestScore();
            if (OnBestScoreUpdated != null) OnBestScoreUpdated();
        }
    }

    public void UpdateCoins(int coins)
    {
        this.coins += coins;
        coinsEarned += coins;
        //SaveCoins();
        if (OnCoinsUpdated != null) OnCoinsUpdated();
    }

    public void UpdateFreeGiftTimestamp(long ticks)
    {
        freeGiftTicks = ticks;
        PlayerPrefs.SetString(StringConstants.FREE_GIFT_TICKS, ticks.ToString());
        PlayerPrefs.Save();
    }

    internal void TutorialCleared(bool status)
    {
        isTutorialCleared = status;
        PlayerPrefs.SetInt(StringConstants.TUTORIAL_STATUS, status == true ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void IncrementGamesPlayed()
    {
        games++;
        PlayerPrefs.SetInt(StringConstants.GAMES_PLAYED, games);
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
        //PlayerPrefs.SetInt(StringConstants.DEATHS, deaths);
        //PlayerPrefs.Save();
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