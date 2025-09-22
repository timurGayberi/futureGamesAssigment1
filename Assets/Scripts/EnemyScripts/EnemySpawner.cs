using UnityEngine;
using MainCharacterScripts;
using scriptableObjects;
using System.Collections;
using generalScripts;

namespace EnemyScripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Factory Reference")]
        [SerializeField] private EnemyFactory enemyFactory;
        
        [Header("Enemy Data")]
        [SerializeField] private EnemyData enemyData;
        
        [Header("Spawn Settings")]
        public float spawnInterval;
        
        [SerializeField]
        private float spawnRadius; 
        
        private Transform _playerTransform;

        private void Start()
        {
            _playerTransform = PlayerController.Instance.transform;
            StartCoroutine(SpawnEnemies());
        }
        
        private IEnumerator SpawnEnemies()
        {

            if (_playerTransform != null)
            {
                while (true)
                {
                    yield return new WaitForSeconds(spawnInterval);

                    Vector3 spawnPosition = _playerTransform.position + (Vector3)(Random.insideUnitCircle.normalized * spawnRadius);
                    IEnemy newEnemy = enemyFactory.CreateMeleeEnemy(spawnPosition);
                
                    if (newEnemy != null)
                    {
                        newEnemy.Initialize(_playerTransform, enemyData);
                    }
                }
            }
            else
            {
                yield return 0;
            }
            
        }
        
    }
}