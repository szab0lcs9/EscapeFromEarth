using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfGold { get; set; }
    public int NumberOfSilver { get; set; }

    public void Collected(string name)
    {
        switch (name.ToLower())
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
    }

}
