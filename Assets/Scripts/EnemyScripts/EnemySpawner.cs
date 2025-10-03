using UnityEngine;
using MainCharacterScripts;
using scriptableObjects;
using System.Collections;
using System.Collections.Generic;
using generalScripts;

namespace EnemyScripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Factory Reference")]
        [SerializeField] private EnemyFactory enemyFactory;
        
        [Header("Spawn Settings")]
        [SerializeField]
        public float spawnInterval;
        [SerializeField]
        private float spawnRadius; 
        
        private Transform _playerTransform;
        private int _enemiesAliveCount = 0; 
        private Coroutine _spawnCoroutine;

        private void Start()
        {
            if (enemyFactory == null)
            {
                return;
            }

            if (DifficultyManager.Instance == null)
            {
                return;
            }

            if (PlayerController.Instance != null)
            {
                _playerTransform = PlayerController.Instance.transform;
                StartCoroutine(SpawnEnemies());
            }
        }
        public void Initialize(Transform player)
        {
            if (_playerTransform == null)
            {
                _playerTransform = player;
                
                if (_spawnCoroutine != null)
                {
                    StopCoroutine(_spawnCoroutine);
                }
                _spawnCoroutine = StartCoroutine(SpawnEnemies());
            }
        }
        
        private IEnumerator SpawnEnemies()
        {
            if (_playerTransform == null)
            {
                Debug.LogError("Player transform not set. Spawner cannot run.");
                yield break;
            }
            
            while (GameManager.Instance != null && GameManager.Instance.CurrentGameState == GameState.Gameplay)
            {
                var rateMultiplier = DifficultyManager.Instance.GetCurrentSpawnRateMultiplier();
                var actualSpawnInterval = spawnInterval / rateMultiplier;
                
                yield return new WaitForSeconds(spawnInterval);

                List<EnemyData> availableEnemies = DifficultyManager.Instance.GetAvailableEnemyTypes();

                if (availableEnemies.Count == 0)
                {
                    continue;
                }
                
                var spawnPosition = _playerTransform.position + (Vector3)(Random.insideUnitSphere.normalized * spawnRadius);
                
                EnemyData enemyData = availableEnemies[Random.Range(0, availableEnemies.Count)];

                IEnemy newEnemy = null;
                EnemyType type = enemyData.enemyType;

                if (type == EnemyType.Melee)
                {
                    newEnemy = enemyFactory.CreateMeleeEnemy(spawnPosition, enemyData, _playerTransform);
                }

                else if (type == EnemyType.Ranged)
                {
                    newEnemy = enemyFactory.CreateRangedEnemy(spawnPosition, enemyData, _playerTransform);
                }

                if (newEnemy == null)
                {
                    Debug.LogWarning("Failed to spawn enemy :(");
                }
                else
                {
                    _enemiesAliveCount++;
                }

            }
        }
        public void OnEnemyDestroyed()
        {
            _enemiesAliveCount--;

            if (DifficultyManager.Instance != null)
            {
                DifficultyManager.Instance.EnemyKilled();
            }
        }
    }
}