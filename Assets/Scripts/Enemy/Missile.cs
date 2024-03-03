using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Missile : MonoBehaviour
{
    public Ammo Ammo { get; set; }

    [SerializeField] GameObject explosionEffect;
    float maxShootingDistance = 4f;

    void Update()
    {
        Launch();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Missile"))
        {
            collision.collider.isTrigger = true;
        }

        if (!collision.gameObject.name.Contains("Missile"))
        {
            collision.gameObject.SendMessage("TakeDamage", Ammo.Damage, SendMessageOptions.DontRequireReceiver);
            Explode();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Contains("Missile"))
        {
            collision.collider.isTrigger = false;
        }

    }

    void Launch()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * Ammo.velocity;

    }

    public void Explode()
    {
        GameObject _explosionEffect = Instantiate(explosionEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(_explosionEffect, 1f);
        Destroy(gameObject);
    }
}
