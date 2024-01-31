using Assets.Scripts.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Asteroid : MonoBehaviour, IEnemy, IDamageable
{
    [SerializeField]
    private float health = 100f;
    public float Health { get => health; set => health = value; }

    private float shield;
    public float Shield { get => shield; set => shield = 0; }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    public void Attack() { }
}
