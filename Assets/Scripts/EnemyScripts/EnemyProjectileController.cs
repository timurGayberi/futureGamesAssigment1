using System;
using UnityEngine;
using scriptableObjects;
using generalScripts;
using MainCharacterScripts;

namespace EnemyScripts
{
    public class EnemyProjectileController : MonoBehaviour
    {
        private WeaponData _data;

        public void Initialize(WeaponData data)
        {
            _data = data;
            
            // Destroy the projectile after a set time to prevent it from flying forever.
            Destroy(gameObject, _data.projectileGetDestroyTime);
        }

        private void Update()
        {
            // Move the projectile forward based on its own rotation.
            // The RangedEnemy is responsible for setting the initial rotation, so we use `transform.up`.
            transform.Translate(transform.up * _data.projectileSpeed * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var playerHealth = other.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(_data.projectileDamage);
                    Debug.Log("Player shot!");
                }
                
                Destroy(gameObject);
            }
            else if (other.CompareTag("projectile"))
            {
                // This is an additional check for collisions between projectiles.
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}