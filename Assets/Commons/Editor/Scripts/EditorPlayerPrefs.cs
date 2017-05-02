
using UnityEditor;
using UnityEngine;

namespace Commons.Editor.Scripts
{
    public class EditorPlayerPrefs
    {
        [MenuItem("Extensions/PlayerPref/ClearAll")]
        public static void ClearAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        [MenuItem("Extensions/PlayerPref/AddCoins")]
        public static void AddCoins()
        {
            PlayerPrefs.SetInt(StringConstants.COINS, 100);
            PlayerPrefs.Save();
        }

        [MenuItem("Extensions/PlayerPref/RemoveCoins")]
        public static void RemoveCoins()
        {
            PlayerPrefs.SetInt(StringConstants.COINS, 0);
            PlayerPrefs.Save();
        }
    }
}
