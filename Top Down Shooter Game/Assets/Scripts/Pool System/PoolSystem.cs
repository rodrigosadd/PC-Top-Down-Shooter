using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    [Header("Bullet Pool variables")]
    public List<Bullet> listBulletPool;
    public Bullet bulletPrefab;
    public int initialAmountBullets;

    [Header("Bullet impact Pool variables")]
    public List<GameObject> listBulletImpactPool;
    public GameObject bulletImpactPrefab;
    public int initialAmountbulletsImpacts;

    [Header("Life bar Pool variables")]
    public List<LifeBarInfo> listLifeBarPool;
    public LifeBarInfo lifeBarPrefab;
    public int initialAmountLifeBars;
    public GameObject lifeBarHolder;

    [Header("Enemy Shooter variables")]
    public List<ShooterEnemy> listEnemyShooterPool;
    public ShooterEnemy enemyShooterPrefab;
    public int initialAmountShooter;

    [Header("Enemy Chaser variables")]
    public List<ChaserEnemy> listEnemyChaserPool;
    public ChaserEnemy enemyChaserPrefab;
    public int initialAmountChaser;

    private GameObject _bulletsHolder;
    private GameObject _bulletsImpactsHolder;
    private GameObject _enemyShooterHolder;
    private GameObject _enemyChaserHolder;

    void Start()
    {
        InitializePool();
    }

    public void InitializePool()
    {
        _bulletsHolder = new GameObject("Bullets Pool");
        _bulletsHolder.transform.position = Vector2.zero;
        for (int index = 0; index <= initialAmountBullets; index++)
        {
            Bullet _bullet = Instantiate(bulletPrefab);
            _bullet.transform.SetParent(_bulletsHolder.transform);
            _bullet.gameObject.SetActive(false);
            listBulletPool.Add(_bullet);
        }

        _bulletsImpactsHolder = new GameObject("Bullets Impacts Pool");
        _bulletsImpactsHolder.transform.position = Vector2.zero;
        for (int index = 0; index <= initialAmountbulletsImpacts; index++)
        {
            GameObject _bulletImpact = Instantiate(bulletImpactPrefab);
            _bulletImpact.transform.SetParent(_bulletsImpactsHolder.transform);
            _bulletImpact.gameObject.SetActive(false);
            listBulletImpactPool.Add(_bulletImpact);
        }

        lifeBarHolder.transform.parent = GameInstances.instance.uiManagerInstance.canvas;
        lifeBarHolder.transform.localScale = Vector2.one;
        for (int index = 0; index <= initialAmountLifeBars; index++)
        {
            LifeBarInfo _lifeBar = Instantiate(lifeBarPrefab, lifeBarHolder.transform);
            _lifeBar.gameObject.SetActive(false);
            listLifeBarPool.Add(_lifeBar);
        }

        _enemyShooterHolder = new GameObject("Enemy Shooter Pool");
        _enemyShooterHolder.transform.position = Vector2.zero;
        for (int index = 0; index <= initialAmountShooter; index++)
        {
            ShooterEnemy _enemyShooter = Instantiate(enemyShooterPrefab, _enemyShooterHolder.transform);
            _enemyShooter.gameObject.SetActive(false);
            listEnemyShooterPool.Add(_enemyShooter);
        }

        _enemyChaserHolder = new GameObject("Enemy Chaser Pool");
        _enemyChaserHolder.transform.position = Vector2.zero;
        for (int index = 0; index <= initialAmountChaser; index++)
        {
            ChaserEnemy _enemyChaser = Instantiate(enemyChaserPrefab, _enemyChaserHolder.transform);
            _enemyChaser.gameObject.SetActive(false);
            listEnemyChaserPool.Add(_enemyChaser);
        }
    }

    public Bullet TryToGetBullet()
    {
        Bullet _toReturn = null;

        for (int index = 0; index < listBulletPool.Count; index++)
        {
            Bullet _possibleBullet = listBulletPool[index];
            if (!_possibleBullet.gameObject.activeSelf)
            {
                _toReturn = _possibleBullet;
                break;
            }
        }

        if (_toReturn == null)
        {
            _toReturn = Instantiate(bulletPrefab);
            _toReturn.transform.SetParent(_bulletsHolder.transform);
            listBulletPool.Add(_toReturn);
        }

        _toReturn.gameObject.SetActive(true);

        return _toReturn;
    }

    public GameObject TryToGetBulletImpact()
    {
        GameObject _toReturn = null;

        for (int index = 0; index < listBulletImpactPool.Count; index++)
        {
            GameObject _possibleBulletImpact = listBulletImpactPool[index];
            if (!_possibleBulletImpact.gameObject.activeSelf)
            {
                _toReturn = _possibleBulletImpact;
                break;
            }
        }

        if (_toReturn == null)
        {
            _toReturn = Instantiate(bulletImpactPrefab);
            _toReturn.transform.SetParent(_bulletsImpactsHolder.transform);
            listBulletImpactPool.Add(_toReturn);
        }

        _toReturn.gameObject.SetActive(true);

        return _toReturn;
    }

    public LifeBarInfo TryToGetLifeBar()
    {
        LifeBarInfo _toReturn = null;

        for (int index = 0; index < listLifeBarPool.Count; index++)
        {
            LifeBarInfo _possibleLifeBar = listLifeBarPool[index];
            if (!_possibleLifeBar.gameObject.activeSelf)
            {
                _toReturn = _possibleLifeBar;
                break;
            }
        }

        if (_toReturn == null)
        {
            _toReturn = Instantiate(lifeBarPrefab);
            _toReturn.transform.SetParent(lifeBarHolder.transform);
            listLifeBarPool.Add(_toReturn);
        }

        _toReturn.gameObject.SetActive(true);

        return _toReturn;
    }

    public ShooterEnemy TryToGetEnemyShooter()
    {
        ShooterEnemy _toReturn = null;

        for (int index = 0; index < listEnemyShooterPool.Count; index++)
        {
            ShooterEnemy _possibleEnemyShooter = listEnemyShooterPool[index];
            if (!_possibleEnemyShooter.gameObject.activeSelf)
            {
                _toReturn = _possibleEnemyShooter;
                break;
            }
        }

        if (_toReturn == null)
        {
            _toReturn = Instantiate(enemyShooterPrefab);
            _toReturn.transform.SetParent(_enemyShooterHolder.transform);
            listEnemyShooterPool.Add(_toReturn);
        }

        _toReturn.gameObject.SetActive(true);

        return _toReturn;
    }

    public ChaserEnemy TryToGetEnemyChaser()
    {
        ChaserEnemy _toReturn = null;

        for (int index = 0; index < listEnemyChaserPool.Count; index++)
        {
            ChaserEnemy _possibleEnemyChaser = listEnemyChaserPool[index];
            if (!_possibleEnemyChaser.gameObject.activeSelf)
            {
                _toReturn = _possibleEnemyChaser;
                break;
            }
        }

        if (_toReturn == null)
        {
            _toReturn = Instantiate(enemyChaserPrefab);
            _toReturn.transform.SetParent(_enemyChaserHolder.transform);
            listEnemyChaserPool.Add(_toReturn);
        }

        _toReturn.gameObject.SetActive(true);

        return _toReturn;
    }
}
