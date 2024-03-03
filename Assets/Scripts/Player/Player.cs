using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    const float HEALTH_DAMAGE_RATIO_WHEN_HAS_SHIELD = 0.1f;
    const float SHIELD_DAMAGE_MULTIPLIER = 0.5f;
    const float REFILL_WAIT_TIME = 10f;

    float initialHealth;
    float initialShield;
    bool hasShield;

    [SerializeField] private float health;
    public float Health { get => health; set => health = value; }

    [SerializeField] private float shield;
    public float Shield { get => shield; set => shield = value; }


    void Start()
    {
        hasShield = true;
        initialHealth = health;
        initialShield = shield;

        StartCoroutine(RefillHealth());
        StartCoroutine(RefillShield());
    }

    public void Die()
    {
        Debug.Log("You Died!");
    }

    public void TakeDamage(float damageTaken)
    {
        if (hasShield)
            shield -= damageTaken * SHIELD_DAMAGE_MULTIPLIER;

        if (shield <= 0f)
        {
            hasShield = false;
            shield = 0;
        }

        health = hasShield ? health - (damageTaken * HEALTH_DAMAGE_RATIO_WHEN_HAS_SHIELD) : health - damageTaken;

        if (health <= 0f)
        {
            Die();
            health = 0;
        }
    }

    IEnumerator RefillHealth()
    {
        yield return new WaitForSeconds(REFILL_WAIT_TIME);

        for (float actualHealth = health; actualHealth <= initialHealth; actualHealth += 0.1f)
        {
            health = actualHealth;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        health = initialHealth;

        StartCoroutine(RefillHealth());
    }
    IEnumerator RefillShield()
    {
        yield return new WaitForSeconds(REFILL_WAIT_TIME);

        for (float actualShield = shield; actualShield <= initialShield; actualShield += 0.01f)
        {
            shield = actualShield;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        shield = initialShield;

        StartCoroutine(RefillShield());
    }

}
