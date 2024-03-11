using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerInventory>(out var inventory))
        {
            inventory.GoldCollected();
            gameObject.SetActive(false);
        }
    }
}
