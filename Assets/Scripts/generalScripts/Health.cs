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
            var floatAmount = (float)amount;

            if (currentHealth >= maxHealth) return;

            var hpValueToAdd = 0f;

            if (currentHealth <= 100f)
            {
                hpValueToAdd = maxHealth -  currentHealth;
            }
            
            else if (currentHealth < (maxHealth - floatAmount))
            {
                hpValueToAdd = floatAmount;
            }
            
            currentHealth = Mathf.Min(currentHealth + hpValueToAdd, maxHealth);
            
            Debug.Log($"Player healed {hpValueToAdd:F1} HP. Current HP: {currentHealth:F1}");
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