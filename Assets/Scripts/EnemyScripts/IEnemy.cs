using scriptableObjects;
using UnityEngine;

namespace EnemyScripts
{
    public enum EnemyType
    {
        Melee,
        Ranged,
        //Others to come...
    }
    
    public interface IEnemy 
    {
        void Initialize(Transform playerTransform, EnemyData enemyData);
        void TakeDamage(float damage);
        void Die();
    }
}
