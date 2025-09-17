using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

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
        
        [Header("Game Manager References")]
        [SerializeField]
        private GameObject gameUIManager;

        [SerializeField]
        private bool isPlayer;
        
        [Header("Events")]
        public UnityEvent onTakeDamage;
        public UnityEvent onDie;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void SetMaxHealth(float newMaxHealth)
        {
            maxHealth = newMaxHealth;
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damageAmount)
        {
            currentHealth -= damageAmount;
            
            onTakeDamage.Invoke();
            
            if (currentHealth <= 0)
            {
                onDie.Invoke();
            }
        }
    }
}