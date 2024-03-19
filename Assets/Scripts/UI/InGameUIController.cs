using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    TextMeshProUGUI collectedGoldText;

    bool isPaused = false;

    void Start()
    {
        collectedGoldText = GameObject.Find("GoldNumber").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (isPaused)
            Time.timeScale = 0f;
    }

    public void UpdateCollectedGoldText(PlayerInventory playerInventory)
    {
        collectedGoldText.text = playerInventory.NumberOfGold.ToString();
    }

    public void PauseTheGameAndShowPauseMenu()
    {
        isPaused = true;
        //ShowPauseMenu();
    }
}
