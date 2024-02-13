using UnityEngine;
using UnityEngine.Pool;

public class Asteroid : MonoBehaviour, IEnemy, IDamageable
{
    ObjectPool<Asteroid> pool;
    const float minForce = -10f;
    const float maxForce = 10f;

    [SerializeField]
    private float intitalVelocity;

    [SerializeField]
    private float health;
    public float Health { get => health; set => health = value; }

    private float shield;
    public float Shield { get => shield; set => shield = 0; }

    void Awake()
    {
        MoveAsteroid();
    }

    public void Initialize(ObjectPool<Asteroid> pool, float health)
    {
        this.pool = pool;
        this.health = health;
    }

    public void Die()
    {
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

    public void Attack() { }


    public void MoveAsteroid()
    {
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(Random.Range(minForce, maxForce), 0.0f, Random.Range(minForce, maxForce));
    }
}
