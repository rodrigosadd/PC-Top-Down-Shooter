using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public ShipState state;

    [Header("Life variables")]
    public float currentLife;
    public float maxLife;

    [Header("Movement variables")]
    public float currentSpeed;
    public float maxSpeed;
    public float acceleration;

    [Header("Bullet variables")]
    public GameObject impactEffect;
    public int damage;

    public void TakeDamage(int amountDamege)
    {
        currentLife -= amountDamege;

        if (currentLife <= 0)
        {
            state = ShipState.DEAD;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.transform.tag == "Bullet")
        {
            TakeDamage(damage);
        }
        GameObject _bulletImpact = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(_bulletImpact);
    }
}
