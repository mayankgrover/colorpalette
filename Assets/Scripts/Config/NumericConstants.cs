using System;

public static class NumericConstants
{
    public static int MIN_REWARD_VIDEO_AD = 20;
    public static int MAX_REWARD_VIDEO_AD = 30;

    public static int COINS_FOR_STAR = 3;
    public static int SECONDS_FOR_STAR = 3;

    public static int FREE_GIFT_CYCLE_SECONDS = 3600 * 4;
    public static int MIN_REWARD_FREE_GIFT = 35;
    public static int MAX_REWARD_FREE_GIFT = 50;

    public static float REVIVE_COUNTDOWN_SECONDS = 5;

    public static int MIN_GAMES_FOR_WATCHING_ADS = 4;

    public static int GAMES_FOR_RATE_US_REMINDER = 4;

    public static float SHOW_STAR_PROBABILITY = 0.75f;

    public static int PN_DAY_2_ID = 1001;
    public static int PN_DAY_3_ID = 1002;
    public static int PN_DAY_5_ID = 1003;
    public static int PN_FREE_GIFT_ID = 2001;

    public static TimeSpan PN_DAY_2 = new TimeSpan(days: 1, hours: 0, minutes: 0, seconds: 0);
    public static TimeSpan PN_DAY_3 = new TimeSpan(days: 2, hours: 0, minutes: 0, seconds: 0);
    public static TimeSpan PN_DAY_5 = new TimeSpan(days: 4, hours: 0, minutes: 0, seconds: 0);

    //public static TimeSpan PN_DAY_2 = new TimeSpan(days: 0, hours: 0, minutes: 1, seconds: 0);
    //public static TimeSpan PN_DAY_3 = new TimeSpan(days: 0, hours: 0, minutes: 2, seconds: 0);
    //public static TimeSpan PN_DAY_5 = new TimeSpan(days: 0, hours: 0, minutes: 4, seconds: 0);
}
