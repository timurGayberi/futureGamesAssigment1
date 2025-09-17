using UnityEngine;

namespace scriptableObjects
{
    [CreateAssetMenu(fileName = "New weapon data", menuName = "Weapons/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [Header("Weapon Identification")] 
        public string weaponName;
        public GameObject projectilePrefab;

        [Header("Firing Properties")]
        public float fireRate,
                     projectileSpeed,
                     projectileGetDestroyTime,
                     projectileDamage;

        [Header("Ammo Properties")] 
        public bool isInfinite,
                    isExplosive;
        public int maxAmmo;
        public  float reloadTime;
        
    }
}
