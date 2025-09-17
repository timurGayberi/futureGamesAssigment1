using UnityEngine;

namespace EnemyScripts
{
    public class ConcreteEnemyFactory : EnemyFactory
    {
        [Header("Enemy prefabs")] 
        [SerializeField] private GameObject meleeEnemyPrefab;
        [SerializeField] private GameObject rangedEnemyPrefab;

        public override IEnemy CreateMeleeEnemy(Vector3 spawnPosition)
        {
            GameObject enemyGo = Instantiate(meleeEnemyPrefab, spawnPosition, Quaternion.identity);
            return enemyGo.GetComponent<IEnemy>();
        }
        
        public override IEnemy CreateRangedEnemy(Vector3 spawnPosition)
        {
            GameObject enemyGo = Instantiate(rangedEnemyPrefab, spawnPosition, Quaternion.identity);
            return enemyGo.GetComponent<IEnemy>();
        }
    }
}