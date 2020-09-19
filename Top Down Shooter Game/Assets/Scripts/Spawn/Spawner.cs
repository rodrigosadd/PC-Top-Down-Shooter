using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public static Spawner spawnerInstance;
    public float gameMinutes;
    public float timeSpawnEnemy;
    public float gamePlayTime;
    public float currentCountdown;
    public bool gameFinish = false;

    private static bool _startApplication = false;
    private float _currentTimeToSpawnEnemy;
    private bool _canSpawn = false;

    void Start()
    {
        if (!_startApplication)
        {
            DontDestroyOnLoad(gameObject);
        }
        if (spawnerInstance == null)
        {
            spawnerInstance = this;
        }
        _startApplication = true;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game" && !gameFinish)
        {
            CountdownToStart();
            SpawnEnemies();
            CheckEndGame();
        }
    }

    public void GamePlayTime()
    {
        gamePlayTime += Time.deltaTime * 1;
    }

    public void CountdownToStart()
    {
        if (currentCountdown < 1)
        {
            currentCountdown += Time.deltaTime / 4.5f;
        }
        else
        {
            _canSpawn = true;
        }
        GameInstances.instance.uiManagerInstance.spawnerCountdown.gameObject.SetActive(true);
    }

    public void CheckEndGame()
    {
        if (gamePlayTime >= gameMinutes * 60 || GameInstances.GetPlayer().state == ShipState.DISABLED)
        {
            EndGame();
        }
        else
        {
            if (currentCountdown >= 1 && GameInstances.GetPlayer().state != ShipState.DISABLED)
            {
                GamePlayTime();
            }
        }
    }

    public void SpawnEnemies()
    {
        if (_canSpawn)
        {
            if (_currentTimeToSpawnEnemy < 1)
            {
                _currentTimeToSpawnEnemy += Time.deltaTime / timeSpawnEnemy;
            }
            else
            {
                _currentTimeToSpawnEnemy = 0;
                SpawnEnemyShooter();
                SpawnEnemyChaser();
            }
        }
    }

    public void EndGame()
    {
        _canSpawn = false;
        gamePlayTime = 0;
        GameInstances.instance.uiManagerInstance.spawnerCountdown.gameObject.SetActive(false);
        GameInstances.instance.uiManagerInstance.endOfSession.gameObject.SetActive(true);
        gameFinish = true;
    }

    public void SpawnEnemyShooter()
    {
        int _randomIndex = Random.Range(0, GameInstances.instance.spawnPoints.Length);
        ShooterEnemy _shooterEnemy = GameInstances.instance.poolSystemInstance.TryToGetEnemyShooter();
        _shooterEnemy.transform.position = GameInstances.instance.spawnPoints[_randomIndex].position;
        _shooterEnemy.colliderShip.enabled = true;
        _shooterEnemy.aiDestination.enabled = true;
        _shooterEnemy.aiDestination.enabled = true;
        _shooterEnemy.aiPath.enabled = true;
        GameInstances.instance.listShooterEnemies.Add(_shooterEnemy);
    }

    public void SpawnEnemyChaser()
    {
        int _randomIndex = Random.Range(0, GameInstances.instance.spawnPoints.Length);
        ChaserEnemy _chaserEnemy = GameInstances.instance.poolSystemInstance.TryToGetEnemyChaser();
        _chaserEnemy.transform.position = GameInstances.instance.spawnPoints[_randomIndex].position;
        _chaserEnemy.colliderShip.enabled = true;
        _chaserEnemy.aiDestination.enabled = true;
        _chaserEnemy.aiDestination.enabled = true;
        _chaserEnemy.aiPath.enabled = true;
        GameInstances.instance.listChaserEnemies.Add(_chaserEnemy);
    }
}
