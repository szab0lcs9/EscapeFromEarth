using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour, ICollectible
{
    public string Name { get => name; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerInventory>(out var inventory))
        {
            inventory.Collected(Name);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0f);
    }
}
