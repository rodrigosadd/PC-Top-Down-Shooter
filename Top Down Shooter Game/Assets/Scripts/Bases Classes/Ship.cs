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

    public void TakeDamage(int amountDamege)
    {
        currentLife -= amountDamege;

        if (currentLife <= 0)
        {
            state = ShipState.DEAD;
        }
    }
}
