using MainCharacterScripts;
using scriptableObjects;
using UnityEngine;

namespace Collectibles
{
    [CreateAssetMenu(fileName = "Experience Data", menuName = "Collectibles/Experience Data")]
    public class ExperienceCollectibleData : CollectiblesData
    {
        [Header("Experience collectible settings")]
        public int expAmount;

        public override void ApplyEffect(GameObject collector)
        {
            if (PlayerLevelController.Instance != null)
            {
                PlayerLevelController.Instance.AddExperience(expAmount);
            }
        }
    }
}
