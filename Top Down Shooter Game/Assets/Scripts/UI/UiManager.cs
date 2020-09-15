using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public List<LifeBarInfo> listLifeBarInfo;
    public Transform canvas;
    public Camera cam;
    public Vector2 offset;

    void Update()
    {
        AddLifeBar();
        SetLife();
        SetLifeBarPosition();
        DisableLifeBar();
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

        for (int i = 0; i < GameInstances.instance.listShootEnemys.Count; i++)
        {
            EnemyShip enemyIndex = GameInstances.instance.listShootEnemys[i];
            if (!HasLifeBar(enemyIndex))
            {
                LifeBarInfo _lifeBar = GameInstances.instance.poolSystemInstance.TryToGetLifeBar();
                _lifeBar.owner = enemyIndex;
                listLifeBarInfo.Add(_lifeBar);
            }
        }

        for (int i = 0; i < GameInstances.instance.listChaserEnemys.Count; i++)
        {
            EnemyShip enemyIndex = GameInstances.instance.listChaserEnemys[i];
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
}
