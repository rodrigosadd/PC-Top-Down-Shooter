using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ship : MonoBehaviour
{
    public ShipState state;
    public Collider2D colliderShip;

    [Header("Life variables")]
    public GameObject[] shipGraphics;
    public SpriteRenderer[] disableSpriteRender;
    public int currentLife;
    public int maxLife;

    [Header("Bullet variables")]
    public GameObject impactEffect;
    public int maxDamageReceived;

    [Header("Point variable")]
    public int amountPoints;

    private GameObject _bulletImpact;
    private float _currentDisableTime;

    public void TakeDamage(int amountDamege)
    {
        currentLife -= amountDamege;

        if (currentLife <= 0)
        {
            state = ShipState.DEAD;
        }
    }

    public void SetMaxLife()
    {
        currentLife = maxLife;
    }

    public void SetShipGraphics()
    {
        if (currentLife > 60)
        {
            for (int i = 0; i < disableSpriteRender.Length; i++)
            {
                disableSpriteRender[i].color = Color.white;
            }

            shipGraphics[0].gameObject.SetActive(true);
            shipGraphics[1].gameObject.SetActive(false);
            shipGraphics[2].gameObject.SetActive(false);
            shipGraphics[3].gameObject.SetActive(false);
        }
        else if (currentLife <= 60 && currentLife > 20)
        {
            shipGraphics[0].gameObject.SetActive(false);
            shipGraphics[1].gameObject.SetActive(true);
            shipGraphics[2].gameObject.SetActive(false);
            shipGraphics[3].gameObject.SetActive(false);
        }
        else if (currentLife <= 20 && currentLife > 0)
        {
            shipGraphics[0].gameObject.SetActive(false);
            shipGraphics[1].gameObject.SetActive(false);
            shipGraphics[2].gameObject.SetActive(true);
            shipGraphics[3].gameObject.SetActive(false);
        }
        else if (currentLife <= 0)
        {
            shipGraphics[0].gameObject.SetActive(false);
            shipGraphics[1].gameObject.SetActive(false);
            shipGraphics[2].gameObject.SetActive(false);
            shipGraphics[3].gameObject.SetActive(true);
        }
    }

    public void Dead()
    {
        if (state == ShipState.DEAD && state != ShipState.DISABLED)
        {
            colliderShip.enabled = false;
            state = ShipState.DISABLED;
            SetScorePoints();
        }
        else if (state == ShipState.DISABLED)
        {
            DisableSpriteRender();
            DisableShip();
        }
    }

    public void SetScorePoints()
    {
        if (GameInstances.GetPlayer().state != ShipState.DISABLED)
        {
            GameInstances.GetPlayer().amountPoints += amountPoints;
        }
    }

    public void DisableShip()
    {
        if (_currentDisableTime < 1)
        {
            _currentDisableTime += Time.deltaTime / 2f;
        }
        else
        {
            _currentDisableTime = 0;
            gameObject.SetActive(false);
        }
    }

    public void DisableSpriteRender()
    {
        for (int i = 0; i < disableSpriteRender.Length; i++)
        {
            Color _colorRender = disableSpriteRender[i].color;
            _colorRender.a -= 0.5f * Time.deltaTime;
            disableSpriteRender[i].color = _colorRender;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.transform.tag == "Bullet")
        {
            TakeDamage(maxDamageReceived);
            hitInfo.gameObject.SetActive(false);
            _bulletImpact = GameInstances.instance.poolSystemInstance.TryToGetBulletImpact();
            _bulletImpact.transform.position = hitInfo.transform.position;
        }
    }
}
