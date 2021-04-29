using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public static Player Player;

    public SpawnManager SpawnManager;
    public CameraFollower CameraFollower;
    public EnemyManager EnemyManager;
    public UIManager UIManager;

    private void Start()
    {
        InitailizeManagers();
    }

    private void InitailizeManagers()
    {
        if (SpawnManager != null)
        {
            Player = SpawnManager.SpawnPlayer();
            Player.Initialize();
            SpawnManager.SpawnEnemies();
        }
        else
        {
            Debug.LogError("SpawnManager is missing.");
            return;
        }

        if (CameraFollower != null && Player != null)
            CameraFollower.Initialize(Player.transform);
        else
            Debug.LogError("CameraFollower is missing.");

        if (EnemyManager != null)
            EnemyManager.Initialize();
        else
            Debug.LogError("EnemyManager is missing.");

        if (UIManager != null)
        {
            UIManager.Initialize();
        }
        else
            Debug.LogError("UImanager is missing.");
    }

    public void StartGame()
    {
        if (Player != null)
        {
            Player.StartPlayer();
            Player.OnDie += Player_OnDie;
        }
        else
            Debug.LogError("Player component is missing on Player object.");

        foreach(var enemy in EnemyManager.Enemies)
        {
            enemy.GetComponent<EnemyBase>().Initialize();
        }
    }

    public void Finish()
    {
        Player.StopPlayer();
        UIManager.Finish();
    }

    private void Player_OnDie()
    {
        UIManager.ShowRestartButton();
    }
}
