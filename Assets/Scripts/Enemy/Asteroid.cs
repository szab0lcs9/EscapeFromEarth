using UnityEngine;
using UnityEngine.Pool;

public class Asteroid : MonoBehaviour, IEnemy, IDamageable
{
    ObjectPool<Asteroid> pool;

    [SerializeField]
    private float health = 100f;
    public float Health { get => health; set => health = value; }

    private float shield;
    public float Shield { get => shield; set => shield = 0; }

    public void SetPool(ObjectPool<Asteroid> pool)
    {
        this.pool = pool;
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
}
