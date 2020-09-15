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
    public LifeBarInfo LifeBarPrefab;
    public int initialAmountLifeBars;

    private GameObject _bulletsHolder;
    private GameObject _bulletsImpactsHolder;
    private GameObject _LifeBarHolder;

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

        _LifeBarHolder = new GameObject("Life bar Pool");
        _LifeBarHolder.transform.parent = GameInstances.instance.uiManagerInstance.canvas;
        _LifeBarHolder.transform.localScale = Vector2.one;
        for (int index = 0; index <= initialAmountLifeBars; index++)
        {
            LifeBarInfo _LifeBar = Instantiate(LifeBarPrefab, _LifeBarHolder.transform);
            _LifeBar.gameObject.SetActive(false);
            listLifeBarPool.Add(_LifeBar);
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
            _toReturn = Instantiate(LifeBarPrefab);
            _toReturn.transform.SetParent(_LifeBarHolder.transform);
            listLifeBarPool.Add(_toReturn);
        }

        _toReturn.gameObject.SetActive(true);

        return _toReturn;
    }
}
