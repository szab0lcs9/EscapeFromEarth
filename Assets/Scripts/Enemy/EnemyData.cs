using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class EnemyData
{
    public float health;
    public float shield;
    public float[] position;
    public float[] rotation;


    public EnemyData(Alien alien)
    {
        this.health = alien.Health;
        this.shield = alien.Shield;

        this.position = new float[]
        {
            alien.transform.position.x,
            alien.transform.position.y,
            alien.transform.position.z,
        };

        this.rotation = new float[]
        {
            alien.transform.rotation.x,
            alien.transform.rotation.y,
            alien.transform.rotation.z,
        };
    }
}
