using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerInventory>(out var inventory))
        {
            inventory.SilverCollected();
            gameObject.SetActive(false);
        }
    }

}
