using UnityEngine;
using scriptableObjects;
using GeneralScripts;

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

        private float _lastBulletShotTime,
                      _lastMissileShotTime,
                      _reloadTimer;

        private int _currentMissileAmmo;

        private void Awake()
        {
            _currentMissileAmmo = missileData.maxAmmo;
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

            if (_reloadTimer > 0)
            {
                _reloadTimer -= Time.deltaTime;
                if (_reloadTimer <= 0)
                {
                    _currentMissileAmmo = missileData.maxAmmo;
                    Debug.Log("Missiles reloaded!");
                }
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
                return Time.time - _lastMissileShotTime >= weaponData.fireRate && _currentMissileAmmo > 0;
            }
            return false;
        }

        private void FireWeapon(WeaponData weaponData)
        {
            if (weaponData == bulletData)
            {
                _lastBulletShotTime = Time.time;
            }
            else if (weaponData == missileData)
            {
                _lastMissileShotTime = Time.time;
                _currentMissileAmmo--;
                if (_currentMissileAmmo == 0)
                {
                    _reloadTimer = missileData.reloadTime;
                    Debug.Log("Out of missiles");
                }
            }

            Instantiate(weaponData.projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        }
    }
}