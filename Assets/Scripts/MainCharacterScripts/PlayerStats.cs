using UnityEngine;

namespace MainCharacterScripts
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        [Header("Core Stats")]
        public float maxHealth,
                     currentHealth,
                     moveSpeed;
        
        [Header("Combat Stats")] 
        public float fireRate,
                     projectileDamage;
        
    }
}


