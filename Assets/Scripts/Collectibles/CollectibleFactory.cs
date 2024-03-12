using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleFactory
{
    public ICollectible SpawnCollectible(GameObject prefab, Vector3 position)
    {
        GameObject instance = Object.Instantiate(prefab, position, Quaternion.identity);
        Collectible newCollectible = instance.GetComponent<Collectible>();

        return newCollectible;
    }
}
