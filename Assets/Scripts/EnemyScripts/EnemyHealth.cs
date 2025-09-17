using UnityEngine;
using scriptableObjects;
using UnityEngine.Serialization;

namespace EnemyScripts
{
    public class EnemyHealth : MonoBehaviour
    {
        private float _currentHealth;
        
        [Header("Enemy References")]
        [SerializeField]
        private MeleeEnemy meleeEnemy;
        [SerializeField]
        private EnemyData enemyData;

        public void SetMaxHealth()
        {
            _currentHealth = enemyData.maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                if (meleeEnemy != null)
                {
                    meleeEnemy.Die();
                }
            }
        }

    }
}
