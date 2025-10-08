using UnityEngine;
using scriptableObjects;
using MainCharacterScripts;

namespace Collectibles
{
    [CreateAssetMenu(fileName = "MissileAmmo Data", menuName = "Collectibles/MissileAmmo Data")]
    public class MissileCollectible : CollectiblesData
    {
        [Header("Missile collectible settings")]
        public int ammoAmount;
        
        public override void ApplyEffect(GameObject collector)
        {
            var playerShooting = collector.GetComponent<PlayerShooting>();
            
            if (PlayerLevelController.Instance != null)
            {
                playerShooting.AddAmmo(ammoAmount);
            }
        }
    }
}