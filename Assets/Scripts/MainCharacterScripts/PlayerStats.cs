using UnityEngine;

namespace MainCharacterScripts
{

    [CreateAssetMenu(fileName = "New Player Stats", menuName = "Player/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        [Header("Core Stats")]
        public float maxHealth = 100f;
        public float moveSpeed = 5f;
        public float rotationSpeed = 100f;

        [Header("Combat Stats")]
        public float fireRate = 0.5f;
        public float projectileDamage = 10f;
    }
}