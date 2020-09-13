using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : EnemyShip
{
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
    }
}
