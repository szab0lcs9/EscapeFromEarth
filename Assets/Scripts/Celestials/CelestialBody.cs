using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>() != null)
        {
            other.gameObject.TryGetComponent(out Player player);

            player?.Die();
        }
    }
}
