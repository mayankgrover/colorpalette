using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LevelCleared: MonoBehaviour
{
    private Text levelCleared;

    void Start()
    {
        levelCleared = GetComponent<Text>();
        levelCleared.enabled = false;
        ControllerEnemies.Instance.ClearedLevel += LevelClearMsg;
        ControllerEnemies.Instance.ForceClearedLevel += ForceClearMsg;
    }

    private void ForceClearMsg()
    {
        levelCleared.text = "Extra Life!";
        levelCleared.enabled = true;
        Invoke("DisableLevelClearedMsg" , 2.5f);
    }

    private void LevelClearMsg()
    {
        levelCleared.text = "Wave Cleared!";
        levelCleared.enabled = true;
        Invoke("DisableLevelClearedMsg" , 2f);
    }

    private void DisableLevelClearedMsg()
    {
        levelCleared.enabled = false;
    }
}
