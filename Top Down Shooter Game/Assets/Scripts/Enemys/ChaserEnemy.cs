using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : EnemyShip
{
    [Header("Damage variable")]
    public int amountDamage;

    private float _distanceBetween;

    void Start()
    {
        currentLife = maxLife;
    }

    void Update()
    {
        EnemyFollowPLayer();
    }

    void FixedUpdate()
    {
        LookAtPlayer();
        EnemyHitsPlayer();
    }

    public void EnemyHitsPlayer()
    {
        _distanceBetween = Vector2.Distance(transform.position, GameInstances.GetPlayer().transform.position);

        if (_distanceBetween <= stoppingDistance)
        {
            GameInstances.GetPlayer().TakeDamage(amountDamage);
        }
    }
}
