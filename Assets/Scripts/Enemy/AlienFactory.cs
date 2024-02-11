using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class AlienFactory : EnemyFactory
    {
        public override IEnemy SpawnEnemy(GameObject prefab, Vector3 position)
        {
            GameObject instance = Object.Instantiate(prefab, position, Quaternion.identity);
            Alien newAlien = instance.GetComponent<Alien>();

            return newAlien;
        }
    }
}
