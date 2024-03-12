using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image healthBarImage;


    void Start()
    {
        healthBarImage = GetComponentInChildren<Image>();
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        float healthPercentage = currentHealth / maxHealth;

        healthBarImage.fillAmount = healthPercentage;
    }
}
