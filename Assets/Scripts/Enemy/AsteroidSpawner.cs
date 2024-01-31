using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemy
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] AsteroidFactory asteroidFactory;

        public GameObject[] asteroidPrefabs;

        public float spawnInterval = 2f;
        public float spawnRadius = 10f;

        private int randomPrefab;


        void Start()
        {
            asteroidFactory = new AsteroidFactory(asteroidPrefabs[randomPrefab]);
            StartCoroutine(SpawnAsteroids());
        }

        private void Update()
        {
            randomPrefab = GetRandomElement(asteroidPrefabs);
            asteroidFactory.AsteroidPrefab = asteroidPrefabs[randomPrefab];
        }


        IEnumerator SpawnAsteroids()
        {
            while (true)
            {
                Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
                Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
                randomPosition.y = 0;

                try
                {
                    Asteroid asteroid = (Asteroid)asteroidFactory.SpawnEnemy(playerPosition + randomPosition);
                }
                catch (NullReferenceException ex)
                {
                    Debug.LogError(ex.Message);
                }

                yield return new WaitForSeconds(spawnInterval);
            }
        }

        int GetRandomElement(GameObject[] objects) => (int)Random.Range(0, objects.Length - 1);
    }
}
