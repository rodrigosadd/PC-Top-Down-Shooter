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
        TargetAINull();
    }

    void Update()
    {
        DisableFindingPlayer();
        if (GameInstances.GetPlayer().state == ShipState.DISABLED)
        {
            return;
        }
        SetShipGraphics();
        Dead();
    }

    void FixedUpdate()
    {
        EnemyHitsPlayer();
    }

    public void EnemyHitsPlayer()
    {
        _distanceBetween = Vector2.Distance(transform.position, GameInstances.GetPlayer().transform.position);

        if (_distanceBetween <= 0.5 && state != ShipState.DISABLED && GameInstances.GetPlayer().state != ShipState.DISABLED)
        {
            GameInstances.GetPlayer().TakeDamage(maxLife);
            TakeDamage(maxLife);
        }
    }
}
