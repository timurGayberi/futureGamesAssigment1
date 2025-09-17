using scriptableObjects;
using UnityEngine;

namespace EnemyScripts
{
    public interface IEnemy 
    {
        void Initialize(Transform playerTransform, EnemyData enemyData);
        void TakeDamage(float damage);
        void Die();
    }
}
