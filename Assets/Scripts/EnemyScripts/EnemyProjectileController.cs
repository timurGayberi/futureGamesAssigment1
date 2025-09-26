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
            
            Destroy(gameObject, _data.projectileGetDestroyTime);
        }

        private void Update()
        {
            transform.Translate(transform.up * (_data.projectileSpeed * Time.deltaTime), Space.World);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var playerHealth = other.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(_data.projectileDamage);
                    //Debug.Log("Player shot!");
                }
                
                Destroy(gameObject);
            }
            else if (other.CompareTag("projectile"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}