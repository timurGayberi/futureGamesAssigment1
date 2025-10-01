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
            if (PlayerLevelController.Instance != null)
            {
                PlayerLevelController.Instance.AddExperience(ammoAmount);
            }
        }
    }
}
