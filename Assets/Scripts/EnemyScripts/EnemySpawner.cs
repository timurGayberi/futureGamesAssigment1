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
        
        [Header("Enemy Data")]
        [Tooltip("Add all enemy data assets you want to spawn in this list.")]
        [SerializeField]
        private List<EnemyData> enemyDataList;
        
        [Header("Spawn Settings")]
        [SerializeField]
        public float spawnInterval;
        [SerializeField]
        private float spawnRadius; 
        
        private Transform _playerTransform;

        private void Start()
        {
            if (enemyFactory == null)
            {
                return;
            }

            if (PlayerController.Instance != null)
            {
                _playerTransform = PlayerController.Instance.transform;
            }
            
            StartCoroutine(SpawnEnemies());
            
        }
        
        private IEnumerator SpawnEnemies()
        {
            
            if (_playerTransform == null)
            {
                Debug.LogError("Player transform not found. Spawner cannot run.");
                yield break;
            }

            if (enemyDataList == null || enemyDataList.Count == 0)
            {
                Debug.LogError("Enemy data list is empty! Cannot spawn any enemies.");
                yield break;
            }

            while (GameManager.Instance.CurrentGameState == GameState.Gameplay)
            {
                yield return new WaitForSeconds(spawnInterval);
                
                Vector3 spawnPosition = _playerTransform.position + (Vector3)(Random.insideUnitSphere.normalized * spawnRadius);

                EnemyData enemyData = enemyDataList[Random.Range(0, enemyDataList.Count)];

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
                    Debug.LogWarning("Failed to spawn an enemy. Check if the enemyData assets are assigned and the factory is working correctly.");
                }

            }
        }
        
    }
}