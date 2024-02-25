using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemy
{
    public class PoolableEnemySpawner : MonoBehaviour
    {
        AlienStateManager alienStateManager;
        ObjectPool<Asteroid> asteroidPool;
        ObjectPool<Alien> alienPool;
        List<Asteroid> activeAsteroids = new List<Asteroid>();
        List<Alien> activeAliens = new List<Alien>();
        Asteroid asteroid;
        Alien alien;
        Vector3 playerPosition;
        Vector3 previousPlayerPosition;
        Vector3 randomPosition;
        Vector3 alienSpawnPosition;
        float releaseRadius = 40f;
        float maxShield = 100f;
        float maxHealth = 100f;
        int maxNumOfAsteroids = 20;
        int maxNumOfAliens = 1;
        int randomPrefab;

        [SerializeField] AsteroidFactory asteroidFactory;
        [SerializeField] AlienFactory alienFactory;
        [SerializeField] GameObject[] asteroidPrefabs;
        [SerializeField] GameObject[] alienPrefabs;
        [SerializeField] float asteroidSpawnRadius = 40f;
        [SerializeField] float asteroidSpawnInterval = 2f;


        void Awake()
        {
            asteroidPool = new ObjectPool<Asteroid>(
                    createFunc: () => (Asteroid)asteroidFactory.SpawnEnemy(asteroidPrefabs[randomPrefab], randomPosition),
                    actionOnRelease: OnReleaseAsteroid,
                    actionOnDestroy: OnDestroyAsteroid,
                    defaultCapacity: maxNumOfAsteroids,
                    maxSize: maxNumOfAsteroids);

            alienPool = new ObjectPool<Alien>(
                    createFunc: () => (Alien)alienFactory.SpawnEnemy(alienPrefabs[0], alienSpawnPosition),
                    actionOnRelease: OnReleaseAlien,
                    actionOnDestroy: OnDestroyAlien,
                    defaultCapacity: maxNumOfAliens,
                    maxSize: maxNumOfAliens);
        }

        void Start()
        {
            asteroidFactory = new AsteroidFactory();
            alienFactory = new AlienFactory();
            alienStateManager = gameObject.AddComponent<AlienStateManager>();

            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            previousPlayerPosition = playerPosition;

            StartCoroutine(SpawnAsteroids());
            StartCoroutine(SpawnAliens());
        }

        void Update()
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            randomPrefab = GetRandomElement(asteroidPrefabs);

            ReleaseAsteroidWhenTooFarFromPlayer();
        }

        #region Pool for Aliens
        IEnumerator SpawnAliens()
        {
            while (true)
            {
                if (alienPool.CountActive < maxNumOfAliens)
                {
                    Vector3 playerMovementDirection = playerPosition - previousPlayerPosition;
                    Vector3 initialPosition = playerPosition + playerMovementDirection.normalized ;

                    if (initialPosition != playerPosition)
                    {
                        alien = alienPool.Get();
                        alien.Initialize(alienPool, maxHealth, maxShield, initialPosition);

                        alienStateManager.Initialize(alien);

                        alien.gameObject.SetActive(true);

                        activeAliens.Add(alien);

                    }

                }
                yield return new WaitForSeconds(asteroidSpawnInterval);
            }
        }

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
        IEnumerator SpawnAsteroids()
        {
            while (true)
            {
                if (asteroidPool.CountActive < maxNumOfAsteroids)
                {

                    randomPosition = Random.insideUnitCircle * asteroidSpawnRadius;
                    randomPosition.y = 0;

                    asteroid = asteroidPool.Get();
                    asteroid.Initialize(asteroidPool, maxHealth);
                    asteroid.transform.position = playerPosition + randomPosition;
                    asteroid.gameObject.SetActive(true);

                    activeAsteroids.Add(asteroid);

                }
                yield return new WaitForSeconds(asteroidSpawnInterval);
            }
        }

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

        // TODO: áthelyezni az Asteroid osztályba
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

        #endregion

        int GetRandomElement(GameObject[] objects) => (int)Random.Range(0, objects.Length);
    }
}
