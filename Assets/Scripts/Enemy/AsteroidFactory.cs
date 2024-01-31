using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class AsteroidFactory : EnemyFactory
    {
        private GameObject asteroidPrefab;

        public AsteroidFactory(GameObject asteroidPrefab)
        {
             this.asteroidPrefab = asteroidPrefab;
        }

        public GameObject AsteroidPrefab { get => asteroidPrefab; set => asteroidPrefab = value; }   // IS IT ADMISSIBLE??

        public override IEnemy SpawnEnemy(Vector3 position)
        {
            GameObject instance = UnityEngine.Object.Instantiate(asteroidPrefab, position, Quaternion.identity);
            Asteroid newAsteroid = instance.GetComponent<Asteroid>();

            return newAsteroid;
        }
    }
}
