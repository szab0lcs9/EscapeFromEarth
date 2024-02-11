using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class AsteroidFactory : EnemyFactory
    {
        public override IEnemy SpawnEnemy(GameObject prefab, Vector3 position)
        {
            GameObject instance = Object.Instantiate(prefab, position, Quaternion.identity);
            Asteroid newAsteroid = instance.GetComponent<Asteroid>();

            return newAsteroid;
        }
    }
}
