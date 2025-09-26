using System.Linq;
using UnityEngine;
using generalScripts;
using scriptableObjects;

namespace EnemyScripts
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class EnemyBase : MonoBehaviour, IEnemy
    {
        [Header("Component References")]
        [SerializeField] protected Health health;
        
        protected Transform playerTransform;
        protected EnemyData enemyData;
        protected float lastAttackTime;
        
        public virtual void Initialize(Transform playerTransform, EnemyData enemyData)
        {
            this.playerTransform = playerTransform;
            this.enemyData = enemyData;
            if (health != null)
            {
                health.SetMaxHealth(enemyData.maxHealth);
                health.onDie.AddListener(Die);
            }
            else
            {
                Debug.LogError($"Health component is missing on {gameObject.name}. Cannot initialize health.");
            }
        }
        
        public void TakeDamage(float damage)
        {
            health.TakeDamage(damage);
        }
        
        public void Die()
        {
            DropItem();
            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore((int)enemyData.scoreValue);
                GameManager.Instance.OnEnemyDestroyed(1);
            }
            
            if (generalScripts.DifficultyManager.Instance != null)
            {
                generalScripts.DifficultyManager.Instance.EnemyKilled();
            }
            
            Destroy(gameObject);
        }
        
        private void DropItem()
        {
            if (Random.value < enemyData.overallNoDropChance)
            {
                Debug.Log("No item dropped due to overallNoDropChance.");
                return;
            }
            
            if (enemyData?.dropConfigurations == null || enemyData.dropConfigurations.Count == 0)
            {
                return;
            }
            
            var validDrops = enemyData.dropConfigurations
                .Where(config => config.dropWeight > 0 && config.itemToDropPrefab != null)
                .ToList();

            if (validDrops.Count == 0) return;
            
            int totalWeight = validDrops.Sum(config => config.dropWeight);
            
            if (totalWeight <= 0) return;
            
            int randomNumber = Random.Range(1, totalWeight + 1);
            
            DropConfiguration selectedDrop = new DropConfiguration();
            
            int currentWeight = 0;
            
            foreach (var config in validDrops)
            {
                currentWeight += config.dropWeight;
                
                if (randomNumber <= currentWeight)
                {
                    selectedDrop = config;
                    break;
                }
            }
            
            if (selectedDrop.itemToDropPrefab != null)
            {
                Instantiate(selectedDrop.itemToDropPrefab, transform.position, Quaternion.identity);
                Debug.Log($"Enemy dropped single item: {selectedDrop.itemToDropPrefab.name} (Weight: {selectedDrop.dropWeight})");
            }
        }
        protected virtual void Update(){}
        private void DealDamageToPlayer(Collider2D other)
        {
            var playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemyData.damage);
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                DealDamageToPlayer(other);
                lastAttackTime = Time.time;
            }
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (Time.time >= lastAttackTime + enemyData.attackCooldown)
                {
                    DealDamageToPlayer(other);
                    lastAttackTime = Time.time;
                }
            }
        }
    }
}
