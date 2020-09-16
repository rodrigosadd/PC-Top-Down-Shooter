using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public int amountShootersEnemies;
        public int amountChasersEnemies;
        public float spawnInterval;
    }

    public SpawnState stateSpawn;
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private int _currentWave = 0;
    private float _searchCountdown = 1f;
    private float _waveCountdown;
    private bool _finishedSpawn;

    void Start()
    {
        _waveCountdown = timeBetweenWaves;
        GameInstances.instance.uiManagerInstance.waveCountdown.gameObject.SetActive(true);
    }

    void Update()
    {
        CheckWaveCompleted();
        CountdownToSpawnWave();
        GameInstances.instance.CheckEnemiesDisabled();
    }

    public void CheckWaveCompleted()
    {
        if (stateSpawn == SpawnState.WAITING)
        {
            if (AllEnemyIsDead() && _finishedSpawn == true)
            {
                WaveCompleted();
                _finishedSpawn = false;
            }
        }
    }

    public void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        _waveCountdown = timeBetweenWaves;
        if (_currentWave + 1 > waves.Length - 1)
        {
            Debug.Log("All waves completed!");
            GameInstances.instance.uiManagerInstance.waveCountdown.gameObject.SetActive(false);
        }
        else
        {
            GameInstances.instance.uiManagerInstance.waveCountdown.gameObject.SetActive(true);
            stateSpawn = SpawnState.COUNTING;
            _currentWave++;
        }
    }

    public void CountdownToSpawnWave()
    {
        if (stateSpawn == SpawnState.COUNTING)
        {
            if (_waveCountdown <= 0)
            {
                StartCoroutine(SpawnWave());
            }
            else
            {
                _waveCountdown -= Time.deltaTime;
            }
        }
    }

    public bool AllEnemyIsDead()
    {
        if (GameInstances.instance.listShooterEnemies.Count > 0 || GameInstances.instance.listChaserEnemies.Count > 0)
        {
            return false;
        }
        return true;
    }

    public IEnumerator SpawnWave()
    {
        Wave _wave = waves[_currentWave];
        Debug.Log("Spawning Wave: " + _wave.waveName);

        stateSpawn = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.amountShootersEnemies; i++)
        {
            SpawnEnemyShooter();
            yield return new WaitForSeconds(_wave.spawnInterval);
        }
        for (int i = 0; i < _wave.amountChasersEnemies; i++)
        {
            SpawnEnemyChaser();
            yield return new WaitForSeconds(_wave.spawnInterval);
        }
        GameInstances.instance.uiManagerInstance.waveCountdown.gameObject.SetActive(false);
        _finishedSpawn = true;
        stateSpawn = SpawnState.WAITING;
    }

    public void SpawnEnemyShooter()
    {
        int _randomSpawn = Random.Range(0, spawnPoints.Length);
        ShooterEnemy _enemyShooter = GameInstances.instance.poolSystemInstance.TryToGetEnemyShooter();
        _enemyShooter.transform.position = spawnPoints[_randomSpawn].position;
        _enemyShooter.colliderShip.enabled = true;
        GameInstances.instance.listShooterEnemies.Add(_enemyShooter);
    }
    public void SpawnEnemyChaser()
    {
        int _randomSpawn = Random.Range(0, spawnPoints.Length);
        ChaserEnemy _enemyChaser = GameInstances.instance.poolSystemInstance.TryToGetEnemyChaser();
        _enemyChaser.transform.position = spawnPoints[_randomSpawn].position;
        _enemyChaser.colliderShip.enabled = true;
        GameInstances.instance.listChaserEnemies.Add(_enemyChaser);
    }
}
