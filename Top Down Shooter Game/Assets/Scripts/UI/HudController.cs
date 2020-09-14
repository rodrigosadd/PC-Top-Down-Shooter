using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [Header("Pool Instance")]
    public Slider lifeSlider;
    public Gradient lifeGradient;
    public Image lifeFill;

    void Start()
    {
        SetMaxLifePlayer();
    }

    void Update()
    {
        SetLifePlayer();
    }

    public void SetLifePlayer()
    {
        lifeSlider.value = GameInstances.GetPlayer().currentLife;
        lifeFill.color = lifeGradient.Evaluate(lifeSlider.normalizedValue);
    }
    public void SetMaxLifePlayer()
    {
        lifeSlider.maxValue = GameInstances.GetPlayer().maxLife;
        lifeSlider.value = GameInstances.GetPlayer().currentLife;
    }
}