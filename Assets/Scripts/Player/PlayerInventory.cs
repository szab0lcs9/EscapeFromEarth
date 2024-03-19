using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public UnityEvent<PlayerInventory> OnCollectedAnyGems;
    public int NumberOfGold { get; set; }
    public int NumberOfSilver { get; set; }

    public void Collected(string name)
    {
        name = name.Remove(name.Length - 7).ToLower();
        Debug.Log(name);
        switch (name)
        {
            case "gold":
                NumberOfGold++;
                break;

            case "silver":
                NumberOfSilver++;
                break;

            default:
                break;
        }
        OnCollectedAnyGems.Invoke(this);
    }
}
