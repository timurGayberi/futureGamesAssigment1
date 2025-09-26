using UnityEngine;
using generalScripts;
using scriptableObjects;

namespace EnemyScripts
{
    public class MeleeEnemy : EnemyBase
    {
        protected override void Update()
        {
            base.Update(); 

            if (playerTransform == null)
            {
                return;
            }
            
            transform.position = Vector2.MoveTowards
            (
                transform.position,
                playerTransform.position,
                enemyData.moveSpeed * Time.deltaTime
            );
        }
    }
}