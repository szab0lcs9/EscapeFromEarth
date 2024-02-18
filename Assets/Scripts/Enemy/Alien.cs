using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Alien : MonoBehaviour, IEnemy, IDamageable
{
    const float HEALTH_DAMAGE_RATIO_WHEN_HAS_SHIELD = 0.1f;

    ObjectPool<Alien> pool;
    new Transform transform;

    [SerializeField]
    private float health;
    public float Health { get => health; set => health = value; }

    [SerializeField]
    private float shield;
    public float Shield { get => shield; set => shield = value; }

    bool hasShield;

    public void Attack()
    {

    }

    public void Die()
    {
        pool.Release(this);
    }

    public void TakeDamage(float damageTaken)
    {
        if (hasShield)
            shield -= damageTaken * 0.5f;

        if (shield < 0)
            hasShield = false;

        health = hasShield ? health - (damageTaken * HEALTH_DAMAGE_RATIO_WHEN_HAS_SHIELD) : health - damageTaken;

        if (health <= 0f)
            Die();
    }

    public void Initialize(ObjectPool<Alien> pool, float health, float shield)
    {
        this.pool = pool;
        this.health = health;
        this.shield = shield;
    }

    void OnEnable()
    {
        hasShield = true;
    }

    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopMovement()
    {

    }

    internal void AvoidFromAsteroids()
    {

    }
}
