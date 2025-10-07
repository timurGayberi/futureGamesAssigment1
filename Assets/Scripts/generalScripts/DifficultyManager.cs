using System.Collections.Generic;
using UnityEngine;
using scriptableObjects;

namespace generalScripts
{
    [System.Serializable]
    public class EnemyTypeUnlockTier
    {
        [Header("Unlock Condition")] 
        public int killCountToUnlock;

        [Header("Enemy Types to Add")]
        public List<EnemyData> enemyTypesToUnlock;
    }
    
    public class DifficultyManager : MonoBehaviour
    {
        public static DifficultyManager Instance {get; private set;}
        
        [Header("--- Enemy Type Progression Tiers ---")]
        [SerializeField]
        private List<EnemyTypeUnlockTier> enemyTypeUnlockTiers = new List<EnemyTypeUnlockTier>();
        
        [Header("--- Automatic Dynamic Scaling ---")]
        public int killsPerScalingStage = 10;
        
        public int maxScalingStages = 100;
        
        [Range(0f, 0.5f)]
        public float statIncreasePerStage = 0.05f; 
        
        [Range(0f, 0.5f)]
        public float spawnRateIncreasePerStage = 0.1f; 
        
        private int _killCount = 0;
        private int _currentTierIndex = 0; 
        
        private readonly List<EnemyData> _availableEnemyTypes = new List<EnemyData>();
        private readonly List<EnemyData> _initialEnemyTypes = new List<EnemyData>();
        
        private int CurrentScalingStage
        {
            get
            {
                var calculatedStage = _killCount / killsPerScalingStage;
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
                _initialEnemyTypes.AddRange(enemyTypeUnlockTiers[0].enemyTypesToUnlock);
                _availableEnemyTypes.AddRange(_initialEnemyTypes);
            }
        }
        public void ResetDifficulty()
        {
            _killCount = 0;
            _currentTierIndex = 0;
            
            _availableEnemyTypes.Clear();
            _availableEnemyTypes.AddRange(_initialEnemyTypes);
        }
        
        public void EnemyKilled()
        {
            var previousStage = CurrentScalingStage;
            
            _killCount++;
            CheckEnemyTypeUnlock();
            
            var currentStage = CurrentScalingStage;
            
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
                    
                }
            }
        }
        public float GetScaledStartValue(float baseValue)
        {
            return baseValue * GetCurrentStatMultiplier();
        }
        public float GetCurrentStatMultiplier()
        {
            return 1.0f + (CurrentScalingStage * statIncreasePerStage);
        }
        public float GetCurrentSpawnRateMultiplier()
        {
            return 1.0f + (CurrentScalingStage * spawnRateIncreasePerStage);
        }
        public List<EnemyData> GetAvailableEnemyTypes()
        {
            return _availableEnemyTypes; }
        
        public bool IsEnemyTypeAvailable(EnemyData enemyData)
        {
            return _availableEnemyTypes.Contains(enemyData);
        }
    }
}
