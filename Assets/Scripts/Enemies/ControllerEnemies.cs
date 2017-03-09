using Commons.Singleton;
using UnityEngine;
using System;

public class ControllerEnemies : MonoSingleton<ControllerEnemies>
{
    private const int defaultGroupIndex = -1;
    private int enemyGroupIndex = defaultGroupIndex;
    private float nextPosition;

    public Action ClearedLevel;

    private ControllerEnemiesGroup activeGroup;
    private ControllerEnemiesGroup[] enemyGroups;

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += GameStarted;
        ControllerMainMenu.Instance.GameEnded += GameEnded;
        LoadAllEnemyGroups();
    }

    private void GameStarted()
    {
        SelectNextGroup();
        ResetStrips();
        ShowActiveGroup();
    }

    private void ShowActiveGroup()
    {
        if (activeGroup != null) activeGroup.Show();
    }

    private void HideActiveGroup()
    {
        if (activeGroup != null) activeGroup.Hide();
    }

    private void LoadAllEnemyGroups()
    {
        enemyGroups = Resources.LoadAll<ControllerEnemiesGroup>("Waves/");
        for(int i = 0; i < enemyGroups.Length; i++) {
            enemyGroups[i] = GameObject.Instantiate<ControllerEnemiesGroup>(enemyGroups[i]);
            enemyGroups[i].Initialize(Vector3.zero, this.transform);
            enemyGroups[i].Hide();
        }
    }

    private void UpdateNextPosition()
    {
        nextPosition = activeGroup.GroupLength;
    }

    private void GameEnded()
    {
        HideActiveGroup();
        enemyGroupIndex = defaultGroupIndex;
        MoveStrips(-transform.position.y);
        ResetStrips();
    }

    void Update() {
        CheckAndMoveIfLevelCleared();
    }

    private void CheckAndMoveIfLevelCleared()
    {
        if(activeGroup != null && activeGroup.IsGroupCleared)
        {
            //Debug.Log("Level cleared");
            activeGroup.Hide();
            if (ClearedLevel != null) {
                ClearedLevel();
            }

            MoveStrips(nextPosition);
            SelectNextGroup();
            ResetStrips();
            ShowActiveGroup();
        }
    }

    private void SelectNextGroup()
    {
        enemyGroupIndex++;
        int nextGroupIndex = enemyGroupIndex;
        if(enemyGroupIndex >= enemyGroups.Length) {
            nextGroupIndex = UnityEngine.Random.Range(1, enemyGroups.Length);
        }
        activeGroup = enemyGroups[nextGroupIndex];
        UpdateNextPosition();
        //Debug.Log("[Enemies] next activeGroup: " + nextGroupIndex);
    }

    private void MoveStrips(float nextPosition)
    {
        Vector3 pos = gameObject.transform.position;
        pos.y += nextPosition;
        gameObject.transform.position = pos;
    }

    private void ResetStrips()
    {
        //Debug.Log("Colors to use: " + ControllerGame.Instance.ColorsToUse);
        if (activeGroup != null) activeGroup.ResetStrips();
    }
}
