using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    [Header("Bullet Pool variables")]
    public List<Bullet> bulletPool;
    public Bullet bulletPrefab;
    public int initialAmountBullets;

    private GameObject _Bulletsholder;

    void Awake()
    {
        InitializePool();
    }

    public void InitializePool()
    {
        _Bulletsholder = new GameObject("Bullets Pool");
        _Bulletsholder.transform.position = Vector2.zero;
        for (int index = 0; index <= initialAmountBullets; index++)
        {
            Bullet _bullet = Instantiate(bulletPrefab);
            _bullet.transform.SetParent(_Bulletsholder.transform);
            _bullet.gameObject.SetActive(false);
            bulletPool.Add(_bullet);
        }
    }

    public Bullet TryToGetBullet()
    {
        Bullet _toReturn = null;

        for (int index = 0; index < bulletPool.Count; index++)
        {
            Bullet _possibleBullet = bulletPool[index];
            if (!_possibleBullet.gameObject.activeSelf)
            {
                _toReturn = _possibleBullet;
                break;
            }
        }

        if (_toReturn == null)
        {
            _toReturn = Instantiate(bulletPrefab);
            _toReturn.transform.SetParent(_Bulletsholder.transform);
            bulletPool.Add(_toReturn);
        }

        _toReturn.gameObject.SetActive(true);

        return _toReturn;
    }
}
