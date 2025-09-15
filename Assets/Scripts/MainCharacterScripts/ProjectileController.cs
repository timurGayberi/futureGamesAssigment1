using UnityEngine;
using scriptableObjects;

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


        private void Start()
        {
            if (body != null)
            {
                GetComponent<Rigidbody2D>().linearVelocity = transform.up * data.projectileSpeed;
            }

            Destroy(gameObject, 5f);

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
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
                //hit.GetComponent<EnemyHealth>().TakeDamage(data.projectileDamage);
            }
            
        }

    }

}
