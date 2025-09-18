using EnemyScripts;
using UnityEngine;

namespace scriptableObjects
{
    [CreateAssetMenu(fileName = "New enemy data", menuName = "Enemy/Enemy data")]
    public class EnemyData : ScriptableObject
    {
        [Header("Enemy type")]
        public EnemyType enemyType;
        //public bool isRanged;
        
        [Header("Enemy stats")]
        public float maxHealth,
                     moveSpeed,
                     damage;
        
        public int scoreValue;

        [Header("Behaviour")] 
        public float attackCooldown;

    }
}
