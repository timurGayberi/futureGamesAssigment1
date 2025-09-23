using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using scriptableObjects;

namespace generalScripts
{
    [System.Serializable]
    public class DifficultyTier
    {
        [Header("Tier requirements")] 
        public int killCountToUnlock;

        [Header("Enemy data (Scriptable objects stats)")]
        public List<EnemyData> enemyTypesData;
    }
    
    public class DifficultyManager : MonoBehaviour
    {
        public static DifficultyManager Instance {get; private set;}
        
        [Header("Difficulty Tiers")]
        [SerializeField]
        private List<DifficultyTier> difficultyTiers;
        
        private int _killCount = 0;
        
        private int _currentTier = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void EnemyKilled()
        {
            _killCount++;
            CheckDifficultyProgression();
        }
        
        private void CheckDifficultyProgression()
        {
            if (_currentTier + 1 < difficultyTiers.Count)
            {
                var nextTier = difficultyTiers[_currentTier + 1];
                if (_killCount >= nextTier.killCountToUnlock)
                {
                    _currentTier++;
                    Debug.Log("Difficulty increased to tier: " + (_currentTier + 1));
                }
            }
        }
    }
}
