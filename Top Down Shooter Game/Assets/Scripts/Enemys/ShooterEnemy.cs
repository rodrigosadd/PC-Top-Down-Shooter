using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : EnemyShip
{
    [Header("Shot variables")]
    public Transform shotPoint;
    public GameObject bulletPrefab;
    public float timeBtwShots;
    public float startTimeBtwShots;
    public float distanceToShoot;

    private float _distanceBetween;

    void Start()
    {
        currentLife = maxLife;
    }

    void Update()
    {
        EnemyFollowPLayer();
        EnemyShooting();
    }

    void FixedUpdate()
    {
        LookAtPlayer();
    }

    public void EnemyShooting()
    {
        _distanceBetween = Vector2.Distance(transform.position, GameInstances.GetPlayer().transform.position);

        if (_distanceBetween < distanceToShoot)
        {
            if (timeBtwShots <= 0)
            {
                Bullet _bullet = GameInstances.instance.poolSystemInstance.TryToGetBullet();
                _bullet.transform.position = shotPoint.position;
                _bullet.rBodyBullet.velocity = shotPoint.up * -1 * _bullet.speed;
                _bullet.currentShooter = transform;
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
}
