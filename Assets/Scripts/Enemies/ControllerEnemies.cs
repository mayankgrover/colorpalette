﻿using Commons.Singleton;
using UnityEngine;
using System;
using Commons.Services;
using System.Collections;

public class ControllerEnemies : MonoSingleton<ControllerEnemies>
{
    private int defaultGroupIndex = 0;
    private int tutorialGroupIndex = -1;
    private int enemyGroupIndex;
    private float nextPosition;

    public Action ClearedLevel;
    public Action ForceClearedLevel;

    private ControllerEnemiesGroup activeGroup;
    private ControllerEnemiesGroup[] enemyGroups;

    public int WavesCleared { get; private set; }
    public int StarsCollected { get; private set; }
    public int StarsSpawned { get; private set; }
    public Wave DeathWave { get; set; }
    public Strip DeathStrip { get; set; }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        ControllerMainMenu.Instance.GameStarted += GameStarted;
        ControllerMainMenu.Instance.GameEnded += GameEnded;
        enemyGroupIndex = PlayerProfile.Instance.IsTutorialCleared ? defaultGroupIndex : tutorialGroupIndex;
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
        StarsSpawned = 0;
        DeathWave = Wave.Tutorial;
        DeathStrip = Strip.None;
    }

    public void StarCollected()
    {
        StarsCollected++;
    }

    private void ShowActiveGroup()
    {
        if (activeGroup != null) {
            activeGroup.Show();
            StarsSpawned += activeGroup.StarsSpawned;
        }
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
        //StartCoroutine(HideActiveGroupWithDelay(activeGroup));
        if (WavesCleared > 0) defaultGroupIndex = 0;
        enemyGroupIndex = defaultGroupIndex;
        MoveStrips(-transform.position.y);
        ResetStrips();
        HideActiveGroup();
    }

    private void HideActiveGroup()
    {
        if (activeGroup != null) activeGroup.Hide();
    }

    void Update()
    {
        if (!ControllerGame.Instance.IsGamePaused) {
            CheckAndMoveIfLevelCleared();
        }
    }

    public void ForceWaveClear()
    {
        HideActiveGroup();
        ResetStrips();
        ShowActiveGroup();
        if (ForceClearedLevel != null) ForceClearedLevel();
    }

    private void CheckAndMoveIfLevelCleared()
    {
        if(activeGroup != null && activeGroup.IsGroupCleared)
        {
            WavesCleared++;
            if (enemyGroupIndex == (int)Wave.Tutorial) TutorialCleared();
            HideActiveGroup();
            if (ClearedLevel != null) {
                ClearedLevel();
                ServiceSounds.Instance.PlaySoundEffect(SoundEffect.Game_Level_Cleared);
            }

            MoveStrips(nextPosition);
            SelectNextGroup();
            ResetStrips();
            ShowActiveGroup();
        }
    }

    private void TutorialCleared()
    {
        PlayerProfile.Instance.TutorialCleared(true);
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
