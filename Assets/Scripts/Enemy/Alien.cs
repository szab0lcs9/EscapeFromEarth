using System;
using UnityEngine;
using UnityEngine.Pool;

public class Alien : MonoBehaviour, IEnemy, IDamageable, IAttackable
{
    const float HEALTH_DAMAGE_RATIO_WHEN_HAS_SHIELD = 0.1f;

    ObjectPool<Alien> alienPool;
    Rigidbody rb;
    new Transform transform;
    Missile missile;

    bool hasShield;

    [SerializeField] GameObject missilePrefab;
    [SerializeField] Transform missileSpawnPoint;
    [SerializeField] float asteroidDetectionRange;
    [SerializeField] float avoidanceForce;

    [SerializeField] private float health;
    public float Health { get => health; set => health = value; }

    [SerializeField] private float shield;

    public float Shield { get => shield; set => shield = value; }

    public void Attack()
    {
        GameObject _missile = Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);
        missile = _missile.GetComponent<Missile>();
        missile.Launch();
    }

    public void Die()
    {
        alienPool.Release(this);
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

    public void Initialize(ObjectPool<Alien> pool, float health, float shield, Vector3 position)
    {
        this.alienPool = pool;
        this.health = health;
        this.shield = shield;
        transform = GetComponent<Transform>();
        transform.position = position;
    }

    void OnEnable()
    {
        hasShield = true;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        gameObject.transform.position.Set(transform.position.x, 0.0f, transform.position.z);
    }

    internal void StopMovement()
    {
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.0f)
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    internal void AvoidFromAsteroids()
    {
        Collider[] asteroids = Physics.OverlapSphere(transform.position, asteroidDetectionRange, LayerMask.GetMask("Asteroid"));

        if (asteroids.Length > 0)
        {
            Vector3 avoidanceVector = Vector3.zero;
            foreach (Collider asteroid in asteroids)
            {
                Vector3 avoidanceDirection = transform.position - asteroid.transform.position;
                avoidanceVector += avoidanceDirection.normalized / avoidanceDirection.magnitude;
                avoidanceVector.y = 0;
            }
            rb.AddForce(avoidanceVector * avoidanceForce, ForceMode.Impulse);
        }
    }
    internal void MoveTowardsPlayer(Transform player)
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.001f);
    }

}
