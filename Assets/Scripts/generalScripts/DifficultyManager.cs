using System.Collections.Generic;
using UnityEngine;
using EnemyScripts;
using System.Linq;
using scriptableObjects;
using Random = UnityEngine.Random;

namespace generalScripts
{
    [System.Serializable]
    public class DifficultyTier
    {
        [Header("Tier requirements")] 
        public int killCountToUnlock;

        [Header("Enemy data (Scriptable objects stats)")]
        public List<EnemyData> enemyTypesData;

        [Header("Spawn Weights")] 
        public List<EnemyType> availableEnemies;
        public List<int> spawnWeights;

    }

    public class DifficultyManager : MonoBehaviour
    {
        public static DifficultyManager Instance {get; private set;}
        
        [Header("Difficulty Tiers")]
        [SerializeField]
        private List<DifficultyTier> difficultyTiers;

        private int _killCount = 0,
                    _currentTier = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void EnemyKilled()
        {
            _killCount++;
            CheckDifficultyProgression();
        }

        private void CheckDifficultyProgression()
        {
            if (_currentTier + 1 < difficultyTiers.Count)
            {
                DifficultyTier nextTier = difficultyTiers[_currentTier + 1];
                if (_killCount >= nextTier.killCountToUnlock)
                {
                    _currentTier++;
                    Debug.Log("Difficulty increased to :" + (_currentTier + 1));
                }
            }
        }

        public EnemyType GetEnemyTypeToSpawn()
        {
            DifficultyTier currentTier = difficultyTiers[_currentTier];
            int totalWeight = currentTier.spawnWeights.Sum();
            int randomNumber = Random.Range(0, totalWeight);

            for (int i = 0; i < currentTier.spawnWeights.Count; i++)
            {
                if (randomNumber < currentTier.spawnWeights[i])
                {
                    return currentTier.availableEnemies[i];
                }
                
                randomNumber -= currentTier.spawnWeights[i];
            }
            
            return currentTier.availableEnemies[0];
        }

        public float GetScoreForEnemy(EnemyType enemyType)
        {
            var currentTier = difficultyTiers[_currentTier];
            var enemyData = currentTier.enemyTypesData.Find(data => data.enemyType == enemyType);
            return enemyData ? enemyData.scoreValue : 0;
        }
    }
}