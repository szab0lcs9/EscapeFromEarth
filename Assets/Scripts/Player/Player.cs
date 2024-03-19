using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable, IExplodable
{
    const float HEALTH_DAMAGE_RATIO_WHEN_HAS_SHIELD = 0.1f;
    const float SHIELD_DAMAGE_MULTIPLIER = 0.5f;
    const float REFILL_WAIT_TIME = 60f;

    float initialHealth;
    float initialShield;
    bool hasShield;
    bool isUnderAttack = false;

    [SerializeField] GameObject explosionParticle;

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

    void Update()
    {

    }

    public void Die()
    {
        Explode();
        GetComponent<PlayerMovement>().StopMovement();
        Debug.Log("You Died!");
    }

    public void TakeDamage(float damageTaken)
    {
        isUnderAttack = true;

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

        UpdateHealthAndShieldBar();

        Invoke(nameof(ResetUnderAttackFlag), REFILL_WAIT_TIME);
    }

    IEnumerator RefillHealth()
    {
        yield return new WaitForSeconds(REFILL_WAIT_TIME);

        for (float actualHealth = health; !isUnderAttack && actualHealth <= initialHealth; actualHealth += 0.1f)
        {
            health = actualHealth;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        health = initialHealth;

        UpdateHealthAndShieldBar();

        StartCoroutine(RefillHealth());
    }
    IEnumerator RefillShield()
    {
        yield return new WaitForSeconds(REFILL_WAIT_TIME);

        for (float actualShield = shield; !isUnderAttack && actualShield <= initialShield; actualShield += 0.01f)
        {
            shield = actualShield;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        shield = initialShield;

        UpdateHealthAndShieldBar();

        StartCoroutine(RefillShield());
    }

    public void Explode()
    {
        GameObject _explosionParticle = Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);

        AudioManager.Instance.PlaySFX("Explode");

        Destroy(_explosionParticle, 2f);
    }

    void UpdateHealthAndShieldBar()
    {
        GetComponent<PlayerUI>().healthBar.GetComponent<HealthBar>().UpdateHealth(health, initialHealth);
        GetComponent<PlayerUI>().shieldBar.GetComponent<ShieldBar>().UpdateShield(shield, initialShield);
    }

    void ResetUnderAttackFlag()
    {
        isUnderAttack = false;
    }

}
