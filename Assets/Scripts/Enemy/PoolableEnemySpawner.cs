using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemy
{
    public class PoolableEnemySpawner : MonoBehaviour
    {
        ObjectPool<Asteroid> asteroidPool;
        ObjectPool<Alien> alienPool;
        List<Asteroid> activeAsteroids = new List<Asteroid>();
        Asteroid asteroid;
        Alien alien;
        Vector3 playerPosition;
        Vector3 randomPosition;
        float releaseRadius = 40f;
        float maxHealth = 100f;
        int randomPrefab;
        int maxNumOfAsteroids = 10;
        int maxNumOfAliens = 10;

        [SerializeField]
        AsteroidFactory asteroidFactory;

        [SerializeField] AlienFactory alienFactory;

        [SerializeField]
        GameObject[] asteroidPrefabs;

        [SerializeField]
        GameObject[] alienPrefabs;

        [SerializeField]
        float spawnRadius = 40f;

        [SerializeField]
        float spawnInterval = 2f;


        void Start()
        {
            asteroidFactory = new AsteroidFactory();

            StartCoroutine(SpawnAsteroids());
        }

        void Awake()
        {
            asteroidPool = new ObjectPool<Asteroid>(
                    createFunc: () => (Asteroid)asteroidFactory.SpawnEnemy(asteroidPrefabs[randomPrefab], randomPosition),
                    actionOnRelease: OnReleaseAsteroid,
                    actionOnDestroy: OnDestroyAsteroid,
                    defaultCapacity: maxNumOfAsteroids,
                    maxSize: maxNumOfAsteroids);

            alienPool = new ObjectPool<Alien>(
                    createFunc: () => (Alien)alienFactory.SpawnEnemy(alienPrefabs[randomPrefab], randomPosition),
                    actionOnRelease: OnReleaseAlien,
                    actionOnDestroy: OnDestroyAlien,
                    defaultCapacity: maxNumOfAliens,
                    maxSize: maxNumOfAliens);
        }


        void Update()
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            randomPrefab = GetRandomElement(asteroidPrefabs);

            ReleaseAsteroidWhenTooFarFromPlayer();
        }

        #region Pool for Aliens
        private void OnDestroyAlien(Alien alien)
        {
            alien.Die();
        }

        private void OnReleaseAlien(Alien alien)
        {
            alien.gameObject.SetActive(false);
        }
        #endregion

        #region Pool for Asteroids
        void OnReleaseAsteroid(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(false);
        }

        private void OnDestroyAsteroid(Asteroid asteroid)
        {
            asteroid.Die();

            for (int i = 0; i < activeAsteroids.Count; i++)
            {
                if (asteroid.Equals(activeAsteroids[i]))
                {
                    activeAsteroids.RemoveAt(i);
                }
            }
        }

        private void ReleaseAsteroidWhenTooFarFromPlayer()
        {
            for (int i = 0; i < activeAsteroids.Count; i++)
            {
                Asteroid _asteroid = activeAsteroids[i];
                Vector3 offset = _asteroid.transform.position - playerPosition;
                float sqrLength = offset.sqrMagnitude;

                if (sqrLength > releaseRadius * releaseRadius)
                {
                    try
                    {
                        activeAsteroids.RemoveAt(i);
                        asteroidPool.Release(_asteroid);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex.Message);
                    }
                }
            }
        }

        IEnumerator SpawnAsteroids()
        {
            while (true)
            {
                if (asteroidPool.CountActive < maxNumOfAsteroids)
                {

                    randomPosition = Random.insideUnitCircle * spawnRadius;
                    randomPosition.y = 0;

                    this.asteroid = asteroidPool.Get();
                    this.asteroid.Initialize(asteroidPool, maxHealth);
                    this.asteroid.transform.position = playerPosition + randomPosition;
                    this.asteroid.gameObject.SetActive(true);

                    activeAsteroids.Add(this.asteroid);

                }
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        #endregion

        int GetRandomElement(GameObject[] objects) => (int)Random.Range(0, objects.Length);
    }
}
