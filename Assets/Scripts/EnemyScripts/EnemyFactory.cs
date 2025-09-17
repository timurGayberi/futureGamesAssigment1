using UnityEngine;

namespace EnemyScripts
{
    public abstract class EnemyFactory : MonoBehaviour
    {
        public abstract IEnemy CreateMeleeEnemy(Vector3 spawnPosition);
        public abstract IEnemy CreateRangedEnemy(Vector3 spawnPosition);

    }
}
