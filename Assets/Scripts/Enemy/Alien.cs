using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Alien : MonoBehaviour, IEnemy, IDamageable
{
    ObjectPool<Alien> pool;

    [SerializeField]
    private float health = 100f;
    public float Health { get => health; set => health = value; }

    [SerializeField]
    private float shield = 100f;
    public float Shield { get => shield; set => shield = value; }


    public void Attack()
    {
        throw new System.NotImplementedException();
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

    public void SetPool(ObjectPool<Alien> pool)
    {
        this.pool = pool;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
