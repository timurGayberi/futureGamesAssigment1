using UnityEngine;
using MainCharacterScripts;
using generalScripts;
using scriptableObjects;

namespace EnemyScripts
{
    public class RangedEnemy : MonoBehaviour, IEnemy
    {
        private Transform _playerTransform;
        private EnemyData _enemyData;
        private float _lastAttackTime;
        
        [SerializeField] private Transform firePoint;
        
        [Header("Component References")]
        [SerializeField]
        private Health health;
        
        public void Initialize(Transform playerTransform, EnemyData enemyData)
        {
            _playerTransform = playerTransform;
            _enemyData = enemyData;
            health.SetMaxHealth(enemyData.maxHealth);
            health.onDie.AddListener(Die);
            
        }

        private void Update()
        {
            if (_playerTransform == null)
            {
                return;
            }
            
            Vector3 directionToPlayer = _playerTransform.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            var distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);

            if (distanceToPlayer > _enemyData.fireRange)
            {
                transform.position = Vector2.MoveTowards
                (
                    transform.position,
                    _playerTransform.position,
                    _enemyData.moveSpeed * Time.deltaTime
                );
            }
            else
            {
                if (Time.time >= _lastAttackTime + _enemyData.attackCooldown)
                {
                    Shoot();
                    _lastAttackTime = Time.time;
                }
            }
        }

        private void Shoot()
        {
            if (_enemyData.weaponData == null || _enemyData.weaponData.projectilePrefab == null)
            {
                Debug.LogWarning("Enemy's WeaponData or ProjectilePrefab is not set!");
                return;
            }
            
            var projectileGo = Instantiate(_enemyData.weaponData.projectilePrefab, firePoint.position, firePoint.rotation);

            var projectile = projectileGo.GetComponent<EnemyProjectileController>();

            if (projectile != null)
            {
                projectile.Initialize(_enemyData.weaponData);
            }
        }

        public void TakeDamage(float damage)
        {
            health.TakeDamage(damage);
        }

        public void Die()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore((int) _enemyData.scoreValue);
                GameManager.Instance.OnEnemyDestroyed(1);
            }
            Destroy(gameObject);
        }
        
        private void DealDamageToPlayer(Collider2D other)
        {
            var playerHealth = other.GetComponent<Health>();
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
