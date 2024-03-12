using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    Image shieldBarImage;

    void Start()
    {
        shieldBarImage = GetComponentInChildren<Image>();
    }

    public void UpdateShield(float currentShield, float maxShield)
    {
        float shieldPercentage = currentShield / maxShield;

        shieldBarImage.fillAmount = shieldPercentage;
    }
}
