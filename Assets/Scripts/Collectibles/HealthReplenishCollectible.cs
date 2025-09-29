using scriptableObjects;
using UnityEngine;
using generalScripts;

namespace Collectibles
{
    [CreateAssetMenu(fileName = "Health replenish", menuName = "Collectibles/Health replenish Data")]
    public class HealthReplenishCollectible : CollectiblesData
    {
        [Header("Health replenish settings")]
        public int healthReplenishAmount;
        public override void ApplyEffect(GameObject collector)
        {
            var healthComponent = collector.GetComponent<Health>();

            if (healthComponent != null)
            {
                healthComponent.Heal(healthReplenishAmount);
            }
        }
    }
}
