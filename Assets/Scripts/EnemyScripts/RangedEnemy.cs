using UnityEngine;

namespace EnemyScripts
{
    public class RangedEnemy : EnemyBase
    {
        [SerializeField] private Transform firePoint;
        
        protected override void Update()
        {
            base.Update();

            if (playerTransform == null)
            {
                return;
            }
            
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            var distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            
            if (distanceToPlayer > enemyData.fireRange)
            {
                transform.position = Vector2.MoveTowards
                (
                    transform.position,
                    playerTransform.position,
                    enemyData.moveSpeed * Time.deltaTime
                );
            }
            else
            {
                if (Time.time >= lastAttackTime + enemyData.attackCooldown)
                {
                    Shoot();
                    lastAttackTime = Time.time;
                }
            }
        }
        
        private void Shoot()
        {
            if (enemyData.weaponData == null || enemyData.weaponData.projectilePrefab == null)
            {
                Debug.LogWarning("Enemy's WeaponData or ProjectilePrefab is not set!");
                return;
            }
            
            var projectileGo = Instantiate(enemyData.weaponData.projectilePrefab, firePoint.position, firePoint.rotation);
            var projectile = projectileGo.GetComponent<EnemyProjectileController>();

            if (projectile != null)
            {
                projectile.Initialize(enemyData.weaponData);
            }
        }
    }
}
