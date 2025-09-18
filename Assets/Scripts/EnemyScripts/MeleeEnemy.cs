using UnityEngine;
using MainCharacterScripts;
using generalScripts;
using scriptableObjects;
using UnityEngine.Serialization;

namespace EnemyScripts
{
    public class MeleeEnemy : MonoBehaviour, IEnemy
    {
        private Transform _playerTransform;
        private EnemyData _enemyData;
        private float _lastAttackTime;
        
        [Header("Component References")]
        [SerializeField]
        private Health health;

        public void Initialize(Transform playerTransform, EnemyData enemyData)
        {
            _playerTransform = playerTransform;
            _enemyData = enemyData;
            health.SetMaxHealth(enemyData.maxHealth);
        }

        private void Update()
        {
            if (_playerTransform == null)
            {
                return;
            }

            transform.position = Vector2.MoveTowards
            (
                transform.position,
                _playerTransform.position,
                _enemyData.moveSpeed * Time.deltaTime
            );
        }

        public void TakeDamage(float damage)
        {
            health.TakeDamage(damage);
            //Debug.Log("Enemy got damaged");
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        private void DealDamageToPlayer(Collider2D other)
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(_enemyData.damage);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                DealDamageToPlayer(other);
                _lastAttackTime = Time.time;
            }
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (Time.time >= _lastAttackTime + _enemyData.attackCooldown)
                {
                    DealDamageToPlayer(other);
                    _lastAttackTime = Time.time;
                }
            }
        }
    }
}