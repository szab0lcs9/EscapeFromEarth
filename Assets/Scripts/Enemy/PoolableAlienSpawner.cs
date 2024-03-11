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
        Transform player;
        Vector3 playerPosition;
        Vector3 previousPlayerPosition;
        Vector3 alienSpawnPosition;
        float maxShield = 100f;
        float maxHealth = 100f;

        [SerializeField] AlienFactory alienFactory;
        [SerializeField] GameObject[] alienPrefabs;
        [SerializeField] int maxNumOfAliens = 1;
        [SerializeField] float spawnInterval = 2f;

        RandomRotator randomRotator;

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

            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerPosition = player.position;
            previousPlayerPosition = playerPosition;

            StartCoroutine(SpawnAliens());
        }

        void Update()
        {
            playerPosition = player.position;
        }

        IEnumerator SpawnAliens()
        {
            while (true)
            {
                if (alienPool.CountActive < maxNumOfAliens)
                {
                    Vector3 playerMovementDirection = playerPosition - previousPlayerPosition;
                    alienSpawnPosition = playerPosition + playerMovementDirection.normalized * Random.Range(5f, 20f);

                    if (alienSpawnPosition != playerPosition)
                    {
                        alien = alienPool.Get();
                        alien.Initialize(alienPool, maxHealth, maxShield, alienSpawnPosition, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));

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
