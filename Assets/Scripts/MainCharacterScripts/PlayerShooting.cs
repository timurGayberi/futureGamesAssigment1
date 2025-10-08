using generalScripts;
using UnityEngine;
using scriptableObjects;
using System; 

namespace MainCharacterScripts
{
    public class PlayerShooting : MonoBehaviour
    {
        [Header("Projectile Spawn Point")]
        [SerializeField]
        private Transform projectileSpawnPoint;

        [Header("Weapon Data")]
        [SerializeField]
        private WeaponData bulletData,
                           missileData;
        
        [Header("Missile Cooldown")]
        public float missileCooldownRate = 0.5f; 
        
        [Header("Player Data")]
        [SerializeField]
        private PlayerStats playerStats;
        
        private float _lastBulletShotTime;
        private float _lastMissileShotTime = -100f;
        
        public static Action<int, int> OnMissileAmmoUpdated; 
        
        public static Action<float> OnMissileCooldownStarted;

        private void Start()
        {
            if (playerStats == null)
            {
                Debug.LogError("PlayerStats reference is missing on PlayerShooting. Disabling component.");
                enabled = false;
                return;
            }
            
            OnMissileAmmoUpdated?.Invoke(playerStats.currentMissileAmount, playerStats.maxMissileAmount);
        }

        private void Update()
        {
            if (KeyboardInputListener.Instance.MouseLeftClick && CanFire(bulletData))
            {
                FireWeapon(bulletData);
            }
            
            if (KeyboardInputListener.Instance.MouseRightClick && CanFire(missileData))
            {
                FireWeapon(missileData);
            }
        }

        private bool CanFire(WeaponData weaponData)
        {
            if (weaponData == bulletData)
            {
                return Time.time - _lastBulletShotTime >= weaponData.fireRate;
            }
            else if (weaponData == missileData)
            {
                bool cooldownReady = Time.time - _lastMissileShotTime >= missileCooldownRate;
                bool hasAmmo = playerStats.currentMissileAmount > 0;
                
                return cooldownReady && hasAmmo;
            }
            return false;
        }

        private void FireWeapon(WeaponData weaponData)
        {
            Instantiate(weaponData.projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            if (weaponData == bulletData)
            {
                _lastBulletShotTime = Time.time;
            }
            else if (weaponData == missileData)
            {
                _lastMissileShotTime = Time.time;
                playerStats.currentMissileAmount--;
                OnMissileAmmoUpdated?.Invoke(playerStats.currentMissileAmount, playerStats.maxMissileAmount);
                OnMissileCooldownStarted?.Invoke(missileCooldownRate);
                if (playerStats.currentMissileAmount == 0)
                {
                    Debug.Log("Out of missiles");
                }
            }
        }
        
        public void AddAmmo(int amount)
        {
            if (playerStats == null)
            {
                return;
            }
            
            if (playerStats.currentMissileAmount >= playerStats.maxMissileAmount)
            {
                Debug.Log("Missile max capacity reached. Ammo not added.");
                return;
            }
            playerStats.currentMissileAmount += amount;
            playerStats.currentMissileAmount = Mathf.Min(playerStats.currentMissileAmount, playerStats.maxMissileAmount);
            OnMissileAmmoUpdated?.Invoke(playerStats.currentMissileAmount, playerStats.maxMissileAmount);
            
            Debug.Log($"Missile ammo restored by {amount}. Current: {playerStats.currentMissileAmount}");
        }
    }
}
