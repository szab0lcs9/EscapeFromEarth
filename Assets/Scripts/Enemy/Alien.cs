using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Pool;

public class Alien : MonoBehaviour, IEnemy, IDamageable, IAttackable, IExplodable
{
    const float HEALTH_DAMAGE_RATIO_WHEN_HAS_SHIELD = 0.1f;
    const float SHIELD_DAMAGE_MULTIPLIER = 0.5f;

    ObjectPool<Alien> alienPool;
    Rigidbody rb;
    new Transform transform;

    bool hasShield;
    bool canShoot;


    [SerializeField] Ammo ammo;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] Transform missileSpawnPoint;
    [SerializeField] GameObject explosionParticle;
    [SerializeField] float asteroidDetectionRange;
    [SerializeField] float avoidanceForce;
    [SerializeField] float shootInterval;

    [SerializeField] private float health;
    public float Health { get => health; set => health = value; }

    [SerializeField] private float shield;
    public float Shield { get => shield; set => shield = value; }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        hasShield = true;
        StartCoroutine(MissileLaunch());
    }

    void FixedUpdate()
    {
        gameObject.transform.position.Set(transform.position.x, 0.0f, transform.position.z);
    }

    public void Initialize(ObjectPool<Alien> pool, float health, float shield, Vector3 position)
    {
        this.alienPool = pool;
        this.health = health;
        this.shield = shield;
        transform = GetComponent<Transform>();
        transform.position = position;
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
    }


    public void Die()
    {
        Explode();
        alienPool.Release(this);
    }

    public void StopMovement()
    {
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.0f)
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void AvoidFromAsteroids()
    {
        Collider[] asteroids = Physics.OverlapSphere(transform.position, asteroidDetectionRange, LayerMask.GetMask("Asteroid"));

        if (asteroids.Length > 0)
        {
            Vector3 avoidanceVector = Vector3.zero;
            foreach (Collider asteroid in asteroids)
            {
                Vector3 avoidanceDirection = transform.position - asteroid.transform.position;
                avoidanceVector += avoidanceDirection.normalized / avoidanceDirection.magnitude;
                avoidanceVector = ChangeVectorDirection(avoidanceVector);
                avoidanceVector.y = 0;
            }
            rb.AddForce(avoidanceVector * avoidanceForce, ForceMode.Impulse);

        }
    }

    private Vector3 ChangeVectorDirection(Vector3 avoidanceVector)
    {
        float magnitude = avoidanceVector.magnitude;
        float x = avoidanceVector.x;
        float z = avoidanceVector.z;

        return new Vector3(z, 0.0f, x);
    }

    // TODO: pathfinder algorithm
    internal void MoveTowardsPlayer(Transform player)
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.01f);


    }

    public void Explode()
    {
        GameObject _explosionParticle = Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
        Destroy(_explosionParticle, 2f);
    }
}
