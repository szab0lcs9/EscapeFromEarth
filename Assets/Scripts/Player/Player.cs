using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    public float Health { get => health; set => health = value; }

    [SerializeField] private float shield;
    public float Shield { get => shield; set => shield = value; }

    public void Die()
    {
        Debug.Log("You Died!");
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;

        if (health < 0)
        {
            Die();
        }
    }

}
