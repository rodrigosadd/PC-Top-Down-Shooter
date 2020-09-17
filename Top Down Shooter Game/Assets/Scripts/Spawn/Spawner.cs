using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public float gameMinutes;
    public float timeSpawnEnemy;
    public float gamePlayeTime;

    private bool _canSpawn = true;
    private bool _startGame = false;
    private bool _startedSpawner = false;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        if (_startGame)
        {
            Timer();
            CheckGameState();
            GameInstances.instance.CheckEnemiesDisabled();
        }

        if (!_startedSpawner && SceneManager.GetActiveScene().name == "Game")
        {
            StartSpawn();
        }
    }

    public void StartSpawn()
    {
        GameInstances.instance.spawnerInstance.gamePlayeTime = 0;
        StartCoroutine(CountdownToStart());
        _startedSpawner = true;
    }

    public IEnumerator CountdownToStart()
    {
        GameInstances.instance.uiManagerInstance.spawnerCountdown.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        GameInstances.instance.uiManagerInstance.spawnerCountdown.SetActive(false);
        _startGame = true;
    }

    public void Timer()
    {
        gamePlayeTime += 1 * Time.deltaTime;
    }

    public void CheckGameState()
    {
        if (gamePlayeTime < 60 * gameMinutes && _canSpawn)
        {
            StartCoroutine(SpawnEnemies());
        }
        else if (gamePlayeTime >= 60 * gameMinutes)
        {
            gamePlayeTime = 0;
            EndGame();
        }
    }

    public IEnumerator SpawnEnemies()
    {
        _canSpawn = false;

        SpawnEnemyShooter();
        SpawnEnemyChaser();

        yield return new WaitForSeconds(timeSpawnEnemy);
        _canSpawn = true;
    }

    public void SpawnEnemyShooter()
    {
        int _randomIndex = Random.Range(0, spawnPoints.Length);
        ShooterEnemy _shooterEnemy = GameInstances.instance.poolSystemInstance.TryToGetEnemyShooter();
        _shooterEnemy.transform.position = spawnPoints[_randomIndex].position;
        _shooterEnemy.colliderShip.enabled = true;
        _shooterEnemy.aiDestination.enabled = true;
        GameInstances.instance.listShooterEnemies.Add(_shooterEnemy);
    }

    public void SpawnEnemyChaser()
    {
        int _randomIndex = Random.Range(0, spawnPoints.Length);
        ChaserEnemy _chaserEnemy = GameInstances.instance.poolSystemInstance.TryToGetEnemyChaser();
        _chaserEnemy.transform.position = spawnPoints[_randomIndex].position;
        _chaserEnemy.colliderShip.enabled = true;
        _chaserEnemy.aiDestination.enabled = true;
        GameInstances.instance.listChaserEnemies.Add(_chaserEnemy);
    }

    public void EndGame()
    {
        GameInstances.instance.uiManagerInstance.endScorePointsText.text = GameInstances.GetPlayer().amountPoints.ToString();
        GameInstances.instance.uiManagerInstance.endOfSession.gameObject.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Game finish!");
    }
}
