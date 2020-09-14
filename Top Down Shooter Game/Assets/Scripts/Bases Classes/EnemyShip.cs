using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyShip : Ship
{
    [Header("Rigidbody variable")]
    public Rigidbody2D rBodyEnemy;

    [Header("Distance variable")]
    public float stoppingDistance;

    private float _distanceBetween;
    private Vector2 _lookAtdirection;

    public void EnemyFollowPLayer()
    {
        _distanceBetween = Vector2.Distance(transform.position, GameInstances.GetPlayer().transform.position);

        if (_distanceBetween > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, GameInstances.GetPlayer().transform.position, Acceleration() * Time.deltaTime);
        }
        else
        {
            currentSpeed -= acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        }
    }

    public void LookAtPlayer()
    {
        _lookAtdirection = new Vector2(GameInstances.GetPlayer().transform.position.x - transform.position.x, GameInstances.GetPlayer().transform.position.y - transform.position.y);
        float _angle = Mathf.Atan2(_lookAtdirection.y, _lookAtdirection.x) * Mathf.Rad2Deg + 90;
        rBodyEnemy.rotation = _angle;
    }

    public float Acceleration()
    {
        currentSpeed += acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        return currentSpeed;
    }
}
