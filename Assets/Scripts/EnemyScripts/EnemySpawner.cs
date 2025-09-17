using UnityEngine;
using scriptableObjects;
using MainCharacterScripts;
using System.Collections;

namespace EnemyScripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Factory Reference")]
        [SerializeField] private EnemyFactory enemyFactory;
        
        [Header("Enemy Data")]
        [SerializeField] private EnemyData enemyData;
        
        [Header("Spawn Settings")]
        public float spawnInterval = 2f;
        
        [SerializeField]
        private float spawnRadius; 
        
        private Transform _playerTransform;

        private void Start()
        {
            // We use the Singleton pattern to get a reference to the player's transform.
            _playerTransform = PlayerController.Instance.transform;
            StartCoroutine(SpawnEnemies());
        }
        
        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnInterval);
                
                // Get a random position on a circle with the specified spawnRadius.
                // We convert the Vector2 to a Vector3 to resolve the ambiguity.
                Vector3 spawnPosition = _playerTransform.position + (Vector3)(Random.insideUnitCircle.normalized * spawnRadius);
                IEnemy newEnemy = enemyFactory.CreateMeleeEnemy(spawnPosition);

                // Initialize the enemy after it has been created.
                if (newEnemy != null)
                {
                    newEnemy.Initialize(_playerTransform, enemyData);
                }
            }
        }
    }
}