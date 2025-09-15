using UnityEngine;

namespace scriptableObjects
{
    [CreateAssetMenu(fileName = "New enemy data", menuName = "Enemy/Enemy data")]
    public class EnemyData : ScriptableObject
    {
        [Header("Enemy stats")]
        //public string name;

        public float maxHealth,
                     moveSpeed,
                     damage;

        [Header("Behaviour")] 
        public bool isRanged;
        
        public float attackCooldown;

    }
}
