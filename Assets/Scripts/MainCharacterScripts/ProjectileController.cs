using UnityEngine;
using scriptableObjects;
using generalScripts;

namespace MainCharacterScripts
{
    public class ProjectileController : MonoBehaviour
    {
        [Header("Projectile Data")]
        
        [SerializeField]
        private WeaponData data;

        [SerializeField] 
        private GameObject explosivePrefab;

        [SerializeField] 
        private float areaDamageRadius;

        [SerializeField] 
        private Rigidbody2D body;
        
        private bool _hasCollided = false;
        
        public float GetDamage()
        {
            return data.projectileDamage;
        }

        private void Start()
        {
            if (body != null)
            {
                GetComponent<Rigidbody2D>().linearVelocity = transform.up * data.projectileSpeed;
            }

            Destroy(gameObject, data.projectileGetDestroyTime);

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Health enemyHealth = other.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(data.projectileDamage);
                }
                
                if (data.isExplosive)
                {
                    ApplyAreaDamage();
                    Instantiate(explosivePrefab, transform.position, Quaternion.identity);
                }
                
                Destroy(gameObject);
            }
        }

        private void ApplyAreaDamage()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, areaDamageRadius);

            foreach (Collider2D hit in colliders)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Health enemyHealth = hit.GetComponent<Health>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(data.projectileDamage);
                    }
                }
            }
        }
    }
}