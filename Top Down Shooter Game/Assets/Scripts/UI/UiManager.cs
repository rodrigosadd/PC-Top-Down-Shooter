﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [Header("Life bar variables")]
    public List<LifeBarInfo> listLifeBarInfo;
    public Transform canvas;
    public Camera cam;
    public Vector2 offset;

    [Header("Score points variables")]
    public TextMeshProUGUI scorePointsText;

    [Header("Wave countdown variables")]
    public GameObject spawnerCountdown;

    void Update()
    {
        AddLifeBar();
        SetLife();
        SetLifeBarPosition();
        DisableLifeBar();
        SetScorePoints();
    }

    public void SetLife()
    {
        for (int i = 0; i < listLifeBarInfo.Count; i++)
        {
            listLifeBarInfo[i].lifeBar.value = listLifeBarInfo[i].owner.currentLife;
            listLifeBarInfo[i].lifeFill.color = listLifeBarInfo[i].lifeGradient.Evaluate(listLifeBarInfo[i].lifeBar.normalizedValue);
        }
    }

    public void AddLifeBar()
    {
        if (!HasLifeBar(GameInstances.GetPlayer()))
        {
            LifeBarInfo _lifeBar = GameInstances.instance.poolSystemInstance.TryToGetLifeBar();
            _lifeBar.owner = GameInstances.GetPlayer();
            listLifeBarInfo.Add(_lifeBar);
        }

        for (int i = 0; i < GameInstances.instance.listShooterEnemies.Count; i++)
        {
            ShooterEnemy enemyIndex = GameInstances.instance.listShooterEnemies[i];
            if (!HasLifeBar(enemyIndex))
            {
                LifeBarInfo _lifeBar = GameInstances.instance.poolSystemInstance.TryToGetLifeBar();
                _lifeBar.owner = enemyIndex;
                listLifeBarInfo.Add(_lifeBar);
            }
        }

        for (int i = 0; i < GameInstances.instance.listChaserEnemies.Count; i++)
        {
            ChaserEnemy enemyIndex = GameInstances.instance.listChaserEnemies[i];
            if (!HasLifeBar(enemyIndex))
            {
                LifeBarInfo _lifeBar = GameInstances.instance.poolSystemInstance.TryToGetLifeBar();
                _lifeBar.owner = enemyIndex;
                listLifeBarInfo.Add(_lifeBar);
            }
        }
    }

    public bool HasLifeBar(Ship ship)
    {
        for (int j = 0; j < listLifeBarInfo.Count; j++)
        {
            if (listLifeBarInfo[j].owner == ship)
            {
                return true;
            }
        }
        return false;
    }

    public void SetLifeBarPosition()
    {
        for (int i = 0; i < listLifeBarInfo.Count; i++)
        {
            Vector2 _screenPosition = cam.WorldToScreenPoint(listLifeBarInfo[i].owner.transform.position);
            listLifeBarInfo[i].transform.position = _screenPosition + offset;
        }
    }

    public void DisableLifeBar()
    {
        for (int i = 0; i < listLifeBarInfo.Count; i++)
        {
            if (listLifeBarInfo[i].owner.state == ShipState.DISABLED)
            {
                listLifeBarInfo[i].gameObject.SetActive(false);
                listLifeBarInfo.RemoveAt(i);
                i--;
            }
        }
    }

    public void SetScorePoints()
    {
        scorePointsText.text = GameInstances.GetPlayer().amountPoints.ToString();
    }
}
