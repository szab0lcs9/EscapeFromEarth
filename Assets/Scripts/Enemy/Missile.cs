using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Missile : MonoBehaviour
{
    [SerializeField] float velocity;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);

        if (collision.gameObject.name.Contains("Missile"))
        {
            collision.collider.isTrigger = true;
        }

        if (!collision.gameObject.name.Contains("Missile"))
        {
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

    public void Launch()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * velocity;
    }

    public void Explode()
    {
        Destroy(gameObject);
    }
}
