using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float shield;
    public float[] postition;
    public float[] rotation;
    public int numberOfGold;

    public PlayerData(Player player, PlayerInventory inventory)
    {
        this.health = player.Health; 
        this.shield = player.Shield;

        this.postition = new float[]
        {
            player.transform.position.x,
            player.transform.position.y,
            player.transform.position.z,
        };

        this.rotation = new float[]
        {
            player.transform.rotation.x,
            player.transform.rotation.y,
            player.transform.rotation.z,
            player.transform.rotation.w
        };

        this.numberOfGold = inventory.NumberOfGold;
    }
}
