using UnityEngine;
using UnityEngine.Events;

namespace generalScripts
{
    public class Health : MonoBehaviour
    {
        [Header("Health Stats")]
        [SerializeField]
        private float maxHealth;
        [SerializeField]
        private float currentHealth;
        
        [Header("Events")]
        public UnityEvent onTakeDamage;
        public UnityEvent onDie;
        private void Awake()
        {
            currentHealth = maxHealth;
            UpdateHealthUI();
        }
        public void SetMaxHealth(float newMaxHealth)
        {
            maxHealth = newMaxHealth;
            currentHealth = maxHealth;
            UpdateHealthUI();
        }
        public void Heal(int amount)
        {
            if (currentHealth >= maxHealth) return;
            
            float startingHealth = currentHealth;
            
            currentHealth = Mathf.Min(currentHealth + (float)amount, maxHealth);
            
            float actualHealth = currentHealth - startingHealth;
            
            Debug.Log($"Player healed {amount:F1} HP. Current HP: {actualHealth:F1}");
            UpdateHealthUI();
        }
        
        public void TakeDamage(float damageAmount)
        {
            currentHealth -= damageAmount;
            
            onTakeDamage.Invoke();
            UpdateHealthUI();
            
            if (currentHealth <= 0)
            {
                Die();
            }
        }
        private void Die()
        {
            onDie.Invoke();
        }
        private void UpdateHealthUI()
        {
            if (gameObject.CompareTag("Player") && GameManager.Instance != null)
            {
                GameManager.Instance.UpdateHealth(currentHealth);
            }
        }
    }
}