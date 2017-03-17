
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
    }
}
