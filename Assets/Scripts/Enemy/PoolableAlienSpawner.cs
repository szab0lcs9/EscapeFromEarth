using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Enemy
{
    public class PoolableAlienSpawner : MonoBehaviour
    {
        AlienStateManager alienStateManager;
        ObjectPool<Alien> alienPool;
        List<Alien> activeAliens = new List<Alien>();
        Alien alien;
        Vector3 playerPosition;
        Vector3 previousPlayerPosition;
        Vector3 alienSpawnPosition;
        float maxShield = 100f;
        float maxHealth = 100f;
        int maxNumOfAliens = 1;

        [SerializeField] AlienFactory alienFactory;
        [SerializeField] GameObject[] alienPrefabs;
        [SerializeField] float spawnInterval = 2f;



        void Awake()
        {
            alienPool = new ObjectPool<Alien>(
                    createFunc: () => (Alien)alienFactory.SpawnEnemy(alienPrefabs[0], alienSpawnPosition),
                    actionOnRelease: OnReleaseAlien,
                    actionOnDestroy: OnDestroyAlien,
                    defaultCapacity: maxNumOfAliens,
                    maxSize: maxNumOfAliens);
        }

        void Start()
        {
            alienFactory = new AlienFactory();
            alienStateManager = gameObject.AddComponent<AlienStateManager>();

            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            previousPlayerPosition = playerPosition;

            StartCoroutine(SpawnAliens());
        }

        void Update()
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        }

        IEnumerator SpawnAliens()
        {
            while (true)
            {
                if (alienPool.CountActive < maxNumOfAliens)
                {
                    Vector3 playerMovementDirection = playerPosition - previousPlayerPosition;
                    Vector3 initialPosition = playerPosition + playerMovementDirection.normalized;

                    if (initialPosition != playerPosition)
                    {
                        alien = alienPool.Get();
                        alien.Initialize(alienPool, maxHealth, maxShield, initialPosition);

                        alienStateManager.Initialize(alien);

                        alien.gameObject.SetActive(true);

                        activeAliens.Add(alien);

                    }

                }
                yield return new WaitForSeconds(spawnInterval);
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
    }
}
