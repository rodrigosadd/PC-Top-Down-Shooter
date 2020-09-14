using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet variables")]
    public Rigidbody2D rBodyBullet;
    public Transform currentShooter;
    public float maxDistanceBtwShooter;
    public float speed;

    private float _distanceBetween;

    void Update()
    {
        CheckDistanceBtwPlayer();
    }

    public void CheckDistanceBtwPlayer()
    {
        _distanceBetween = Vector2.Distance(transform.position, GameInstances.GetPlayer().transform.position);

        if (_distanceBetween > maxDistanceBtwShooter)
        {
            gameObject.SetActive(false);
        }
    }
}