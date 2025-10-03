using System.Collections.Generic;
using UnityEngine;
using scriptableObjects;

namespace generalScripts
{
    [System.Serializable]
    public class EnemyTypeUnlockTier
    {
        [Header("Unlock Condition")] 
        [Tooltip("Total kills required to UNLOCK the enemy types listed below.")]
        public int killCountToUnlock;

        [Header("Enemy Types to Add")]
        [Tooltip("The list of new enemy types to make available for spawning once this tier is reached.")]
        public List<EnemyData> enemyTypesToUnlock;
    }
    
    public class DifficultyManager : MonoBehaviour
    {
        public static DifficultyManager Instance {get; private set;}
        
        [Header("--- Enemy Type Progression Tiers ---")]
        [Tooltip("Define kill counts at which new, distinct enemy types are added to the spawn pool.")]
        [SerializeField]
        private List<EnemyTypeUnlockTier> enemyTypeUnlockTiers = new List<EnemyTypeUnlockTier>();
        
        [Header("--- Automatic Dynamic Scaling ---")]
        [Tooltip("The number of kills needed to advance the scaling difficulty by one stage.")]
        public int killsPerScalingStage = 10;

        [Tooltip("The maximum number of scaling stages. Set to 0 for no limit (not recommended).")]
        public int maxScalingStages = 100;
        
        [Tooltip("Percent increase in enemy stats (HP, Damage) per stage (e.g., 0.05 for 5%).")]
        [Range(0f, 0.5f)]
        public float statIncreasePerStage = 0.05f; 

        [Tooltip("Percent increase in spawn rate/frequency per stage (e.g., 0.1 for 10% faster).")]
        [Range(0f, 0.5f)]
        public float spawnRateIncreasePerStage = 0.1f; 
        
        private int _killCount = 0;
        private int _currentTierIndex = 0; 
        
        private readonly List<EnemyData> _availableEnemyTypes = new List<EnemyData>();
        
        private int CurrentScalingStage
        {
            get
            {
                int calculatedStage = _killCount / killsPerScalingStage;
                if (maxScalingStages > 0)
                {
                    return Mathf.Min(calculatedStage, maxScalingStages);
                }
                return calculatedStage;
            }
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            
            if (enemyTypeUnlockTiers.Count > 0)
            {
                _availableEnemyTypes.AddRange(enemyTypeUnlockTiers[0].enemyTypesToUnlock);
            }
        }
        
        public void EnemyKilled()
        {
            int previousStage = CurrentScalingStage;
            
            _killCount++;
            CheckEnemyTypeUnlock();
            
            int currentStage = CurrentScalingStage;

            // Only log if the stage actually changed AND we are not at the max cap
            if (currentStage > previousStage)
            {
                Debug.Log($"Difficulty Stage INCREASED! Current Stage: {currentStage}. Stats and spawn rate buffed.");
            }
            else if (currentStage == maxScalingStages && previousStage < maxScalingStages)
            {
                Debug.Log($"Difficulty reached MAX STAGE: {maxScalingStages}. Scaling halted.");
            }
        }
        
        private void CheckEnemyTypeUnlock()
        {
            if (_currentTierIndex + 1 < enemyTypeUnlockTiers.Count)
            {
                var nextTier = enemyTypeUnlockTiers[_currentTierIndex + 1];
                if (_killCount >= nextTier.killCountToUnlock)
                {
                    _currentTierIndex++;
                    _availableEnemyTypes.AddRange(nextTier.enemyTypesToUnlock);
                    
                    Debug.Log($"New Enemy Types UNLOCKED! Tier {_currentTierIndex + 1}. Kill Count: {_killCount}");
                }
            }
        }
        
        public float GetCurrentStatMultiplier()
        {
            return 1.0f + (CurrentScalingStage * 0.15f);
        }
        
        public float GetCurrentSpawnRateMultiplier()
        {
            return 1.0f + (CurrentScalingStage * spawnRateIncreasePerStage);
        }
        
        public List<EnemyData> GetAvailableEnemyTypes()
        {
            return _availableEnemyTypes;
        }
        
        public bool IsEnemyTypeAvailable(EnemyData enemyData)
        {
            return _availableEnemyTypes.Contains(enemyData);
        }
    }
}
