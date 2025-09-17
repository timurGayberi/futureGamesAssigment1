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
        
        [Header("UI References")]
        [SerializeField]
        private TextMeshProUGUI healthText;
        
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
        
        private void Die()
        {
            onDie.Invoke();
        }

        private void UpdateHealthUI()
        {
            if (healthText != null)
            {
                healthText.text = "Health: " + Mathf.Max(0, currentHealth).ToString("F0");
            }
        }
    }
}