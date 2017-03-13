﻿using Commons.Singleton;
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

    public int WavesCleared { get; private set; }
    public int StarsCollected { get; private set; }
    public int StarsSpawned { get; private set; }
    public Wave DeathWave { get; set; }
    public Strip DeathStrip { get; set; }

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += GameStarted;
        ControllerMainMenu.Instance.GameEnded += GameEnded;
        LoadAllEnemyGroups();
    }

    private void GameStarted()
    {
        ResetAnalyticsStats();
        SelectNextGroup();
        ResetStrips();
        ShowActiveGroup();
    }

    private void ResetAnalyticsStats()
    {
        WavesCleared = 0;
        StarsCollected = 0;
        StarsSpawned = 1;
        DeathWave = Wave.None;
        DeathStrip = Strip.None;
    }

    internal void StarCollected()
    {
        StarsCollected++;
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
        DeathWave = activeGroup.wave;
        HideActiveGroup();
        enemyGroupIndex = defaultGroupIndex;
        MoveStrips(-transform.position.y);
        ResetStrips();
    }

    void Update()
    {
        if (!ControllerGame.Instance.IsGamePaused) {
            CheckAndMoveIfLevelCleared();
        }
    }

    private void CheckAndMoveIfLevelCleared()
    {
        if(activeGroup != null && activeGroup.IsGroupCleared)
        {
            WavesCleared++;
            StarsSpawned++;

            activeGroup.Hide();
            if (ClearedLevel != null) ClearedLevel();

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
        DeathWave = activeGroup.wave; // hack
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
