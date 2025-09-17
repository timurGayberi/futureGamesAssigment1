using UnityEngine;
using MainCharacterScripts;
using generalScripts;
using scriptableObjects;

namespace EnemyScripts
{
    public class MeleeEnemy : MonoBehaviour, IEnemy
    {
        private Transform _playerTransform;
        private EnemyData _enemyData;

        [Header("Component References")]
        [SerializeField]
        private Health _health;

        public void Initialize(Transform playerTransform, EnemyData enemyData)
        {
            _playerTransform = playerTransform;
            _enemyData = enemyData;
            _health.SetMaxHealth(enemyData.maxHealth);
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
            _health.TakeDamage(damage);
            Debug.Log("Enemy got damaged!");
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // A simple debug log to confirm the collision method is being called.
            Debug.Log("Collision Detected!");

            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Enemy collided with player. Dealing damage.");
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(_enemyData.damage);
                }
            }
            else if (collision.gameObject.CompareTag("projectile"))
            {
                Debug.Log("Enemy collided with projectile. Taking damage.");
                ProjectileController projectile = collision.gameObject.GetComponent<ProjectileController>();
                if (projectile != null)
                {
                    _health.TakeDamage(projectile.GetDamage());
                }
            }
        }
    }
}