using UnityEngine;
using scriptableObjects;

namespace EnemyScripts
{
    public class ConcreteEnemyFactory : EnemyFactory
    {
        [Header("Enemy prefabs")]
        [SerializeField] private GameObject meleeEnemyPrefab;
        [SerializeField] private GameObject rangedEnemyPrefab;

        public override IEnemy CreateMeleeEnemy(Vector3 spawnPosition, EnemyData enemyData, Transform playerTransform)
        {
            GameObject enemyGo = Instantiate(meleeEnemyPrefab, spawnPosition, Quaternion.identity);
            
            // Get the IEnemy component and initialize it with the data and player transform
            IEnemy newEnemy = enemyGo.GetComponent<IEnemy>();
            if (newEnemy != null)
            {
                newEnemy.Initialize(playerTransform, enemyData);
            }
            else
            {
                Debug.LogError("Melee enemy prefab is missing an IEnemy component!");
            }
            
            return newEnemy;
        }

        public override IEnemy CreateRangedEnemy(Vector3 spawnPosition, EnemyData enemyData, Transform playerTransform)
        {
            GameObject enemyGo = Instantiate(rangedEnemyPrefab, spawnPosition, Quaternion.identity);
            
            // Get the IEnemy component and initialize it with the data and player transform
            IEnemy newEnemy = enemyGo.GetComponent<IEnemy>();
            if (newEnemy != null)
            {
                newEnemy.Initialize(playerTransform, enemyData);
            }
            else
            {
                Debug.LogError("Ranged enemy prefab is missing an IEnemy component!");
            }
            
            return newEnemy;
        }
    }
}