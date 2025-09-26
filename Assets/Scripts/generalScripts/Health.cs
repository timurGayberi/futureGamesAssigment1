using UnityEngine;
using UnityEngine.Events;
using TMPro;

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
        
        /*public void Heal(float amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
            UpdateHealthUI();
        }*/
        
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