using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfGold { get; private set; }
    public int NumberOfSilver { get; private set; }

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
