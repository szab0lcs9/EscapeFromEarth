using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class Alien : MonoBehaviour, IEnemy, IDamageable, IAttackable, IExplodable
{
    const float HEALTH_DAMAGE_RATIO_WHEN_HAS_SHIELD = 0.1f;
    const float SHIELD_DAMAGE_MULTIPLIER = 0.5f;

    ObjectPool<Alien> alienPool;
    GameObject healthBar;
    GameObject shieldBar;
    Vector3 barPositionAdjust = new Vector3(0f, 3f, 0f);
    Vector3 barYPosition = new Vector3(0f, 15f, 0f);


    float initialHealth;
    float initialShield;

    bool hasShield;
    bool canShoot;

    [SerializeField] Ammo ammo;
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] GameObject shieldBarPrefab;
    [SerializeField] GameObject explosionParticle;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] Transform missileSpawnPoint;
    [SerializeField] float shootInterval;

    [SerializeField] private float health;
    public float Health { get => health; set => health = value; }

    [SerializeField] private float shield;
    public float Shield { get => shield; set => shield = value; }


    void Start()
    {
        healthBar = Instantiate(healthBarPrefab, FindObjectOfType<Canvas>().transform);
        shieldBar = Instantiate(shieldBarPrefab, FindObjectOfType<Canvas>().transform);

        initialHealth = health;
        initialShield = shield;
    }

    void OnEnable()
    {
        hasShield = true;
        StartCoroutine(MissileLaunch());
    }

    void Update()
    {
        ShowBarsOnScreen();
    }

    private void ShowBarsOnScreen()
    {
        Camera mainCamera = FindObjectOfType<Camera>();
        Vector3 objectViewportPos = mainCamera.WorldToViewportPoint(transform.position);

        if (objectViewportPos.x >= 0 && objectViewportPos.x <= 1 && objectViewportPos.y >= 0 && objectViewportPos.y <= 1 && objectViewportPos.z > 0)
        {
            healthBar.SetActive(true);
            shieldBar.SetActive(true);
            healthBar.transform.position = mainCamera.WorldToScreenPoint(transform.position) + barYPosition - barPositionAdjust;
            shieldBar.transform.position = mainCamera.WorldToScreenPoint(transform.position) + barYPosition + barPositionAdjust;
        }
        else
        {
            healthBar.SetActive(false);
            shieldBar.SetActive(false);
        }
    }

    public void Initialize(ObjectPool<Alien> pool, float health, float shield, Vector3 position, Quaternion rotation)
    {
        this.alienPool = pool;
        this.health = health;
        this.shield = shield;
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Attack()
    {
        canShoot = true;
    }

    IEnumerator MissileLaunch()
    {
        yield return new WaitForSeconds(shootInterval);
        if (canShoot)
        {
            GameObject _missile = Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);
            _missile.GetComponent<Missile>().Ammo = ammo;
            canShoot = false;
        }

        StartCoroutine(MissileLaunch());
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
            Die();

        UpdateHealthAndShieldBar();
    }

    public void Die()
    {
        Explode();
        alienPool.Release(this);
    }

    public void Explode()
    {
        GameObject _explosionParticle = Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
        Destroy(_explosionParticle, 2f);
    }

    void UpdateHealthAndShieldBar()
    {
        healthBar.GetComponent<HealthBar>().UpdateHealth(health, initialHealth);
        shieldBar.GetComponent<ShieldBar>().UpdateShield(shield, initialShield);
    }

}
