using UnityEngine;
using UnityEngine.Pool;

public class Asteroid : MonoBehaviour, IEnemy, IDamageable, IExplodable
{

    ObjectPool<Asteroid> pool;
    CollectibleFactory collectibleFactory;
    const float minForce = -10f;
    const float maxForce = 10f;

    [SerializeField] GameObject explosionParticle;
    [SerializeField] GameObject collectiblePrefab;
    [SerializeField] float intitalVelocity;

    [SerializeField] float health;
    public float Health { get => health; set => health = value; }

    float shield;
    public float Shield { get => shield; set => shield = 0; }

    void Awake()
    {
        collectibleFactory = new CollectibleFactory();
        MoveAsteroid();
    }

    public void Initialize(ObjectPool<Asteroid> pool, float health)
    {
        this.pool = pool;
        this.health = health;
    }

    public void Die()
    {
        collectibleFactory.SpawnCollectible(collectiblePrefab, transform.position);
        pool.Release(this);
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        if (health <= 0f)
        {
            Die();
        }
    }

    public void MoveAsteroid()
    {
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(Random.Range(minForce, maxForce), 0.0f, Random.Range(minForce, maxForce));
    }

    public void Explode()
    {
        GameObject _explosionParticle = Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
        Destroy(_explosionParticle, 2f);
    }
}
