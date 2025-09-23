using UnityEngine;
using scriptableObjects;


namespace EnemyScripts
{
    public abstract class EnemyFactory : MonoBehaviour
    {
        public abstract IEnemy CreateMeleeEnemy(Vector3 spawnPosition, EnemyData enemyData, Transform playerTransform);
        public abstract IEnemy CreateRangedEnemy(Vector3 spawnPosition, EnemyData enemyData, Transform playerTransform);
    }
}