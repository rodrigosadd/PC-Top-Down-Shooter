using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstances : MonoBehaviour
{
    [Header("Player Instance")]
    public PlayerController playerInstance;

    [Header("Pool Instance")]
    public PoolSystem poolSystemInstance;

    [Header("UI Manager Instance")]
    public UiManager uiManagerInstance;

    [Header("Enemys Instances")]
    public List<ShooterEnemy> listShooterEnemies;
    public List<ChaserEnemy> listChaserEnemies;
    public Transform[] spawnPoints;

    public static GameInstances instance;

    void Awake()
    {
        instance = this;
    }

    public static PlayerController GetPlayer()
    {
        return instance.playerInstance;
    }

    public void CheckEnemiesDisabled()
    {
        for (int i = 0; i < listShooterEnemies.Count; i++)
        {
            ShooterEnemy _shooterEnemy = listShooterEnemies[i];
            if (!_shooterEnemy.gameObject.activeSelf)
            {
                listShooterEnemies.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < listChaserEnemies.Count; i++)
        {
            ChaserEnemy _chaserEnemy = listChaserEnemies[i];
            if (!_chaserEnemy.gameObject.activeSelf)
            {
                listChaserEnemies.RemoveAt(i);
                i--;
            }
        }
    }
}
