using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : EnemyShip
{
    private float _distanceBetween;

    void OnEnable()
    {
        state = ShipState.NORMAL;
        SetMaxLife();
    }

    void Update()
    {
        SetShipGraphics();
        Dead();
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

        if (_distanceBetween <= stoppingDistance && state != ShipState.DISABLED && GameInstances.GetPlayer().state != ShipState.DISABLED)
        {
            GameInstances.GetPlayer().TakeDamage(maxLife);
            TakeDamage(maxLife);
        }
    }
}
