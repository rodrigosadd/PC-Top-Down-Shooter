using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstances : MonoBehaviour
{
    [Header("Player Instance")]
    public PlayerController playerInstance;

    [Header("Pool Instance")]
    public PoolSystem poolSystemInstance;

    [Header("Hud controller Instance")]
    public HudController hudControllerInstance;

    [Header("Enemys Instances")]
    public List<ShooterEnemy> listShootEnemys;
    public List<ChaserEnemy> listChaserEnemys;

    public static GameInstances instance;

    void Awake()
    {
        instance = this;
    }

    public static PlayerController GetPlayer()
    {
        return instance.playerInstance;
    }
}
