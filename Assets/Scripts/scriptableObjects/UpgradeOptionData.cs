using UnityEngine;

namespace scriptableObjects
{
    public enum UpgradeType
    {
        //General Stats
        
            //Health Upgrades 
            IncreaseMaxHealth,
        
            //Movement Upgrades
            IncreaseMovementAndRotationSpeed,
        
            //Utility
            UpgradeExperienceCollectible,
            UpgradeHealthCollectible,
        
        //Battle Upgrades
        
            //General Battle Upgrades
            IncreaseBaseDamage,
            IncreaseShootingSpeed,
            IncreaseShootingRange,
        
            //Missile Upgrades
            IncreaseMissileCapacity,
            IncreaseAreaDamage,
            DecreaseReloadTime,
        
    }
    
    [CreateAssetMenu(fileName = "Upgrades Data", menuName = "Upgrades/Upgrades Data")]
    public class UpgradeOptionData : ScriptableObject
    {
        [Header("UI Display")]
        public string upgradeName;
        
        [TextArea(3, 5)]
        public string upgradeDescription;
        
        [Header("Effect Data")]
        public UpgradeType upgradeType;

        public float effectValue;
    }
}
