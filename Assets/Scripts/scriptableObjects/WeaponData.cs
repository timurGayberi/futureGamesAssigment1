using UnityEngine;

namespace scriptableObjects
{
    [CreateAssetMenu(fileName = "New weapon data", menuName = "Weapons/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [Header("Weapon Identification")] 
        public string weaponName;
        public GameObject projectilePrefab,
                          explosivePrefab;

        [Header("Firing Properties")]
        public float fireRate,
                     projectileSpeed,
                     projectileGetDestroyTime,
                     projectileDamage,
                     areaDamageRadius;

        [Header("Ammo Properties")] 
        public bool isInfinite,
                    isExplosive;
    }
}
