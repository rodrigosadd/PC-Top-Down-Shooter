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
    public SpriteRenderer[] destroyedSpriteRender;
    public int currentLife;
    public int maxLife;

    [Header("Bullet variables")]
    public GameObject impactEffect;
    public int damage;

    [Header("Point variable")]
    public int amountPoints;

    private GameObject _bulletImpact;

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
            for (int i = 0; i < destroyedSpriteRender.Length; i++)
            {
                destroyedSpriteRender[i].color = Color.white;
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

            if (GameInstances.GetPlayer().state != ShipState.DISABLED)
            {
                GameInstances.GetPlayer().amountPoints += amountPoints;
            }
        }
        else if (state == ShipState.DISABLED)
        {
            DestroySpriteRender();
            StartCoroutine(DisableShip());
        }
    }

    public IEnumerator DisableShip()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    public void DestroySpriteRender()
    {
        for (int i = 0; i < destroyedSpriteRender.Length; i++)
        {
            Color _colorRender = destroyedSpriteRender[i].color;
            _colorRender.a -= 0.5f * Time.deltaTime;
            destroyedSpriteRender[i].color = _colorRender;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.transform.tag == "Bullet")
        {
            TakeDamage(damage);
            hitInfo.gameObject.SetActive(false);
            _bulletImpact = GameInstances.instance.poolSystemInstance.TryToGetBulletImpact();
            _bulletImpact.transform.position = hitInfo.transform.position;
        }
    }
}
