using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using generalScripts.Interfaces;
using scriptableObjects;

namespace generalScripts
{
    public class LocalJsonBackend : IDataBackend
    {
        //private PlayerLeaderboard leaderboard = new PlayerLeaderboard();
        private string saveFilePath;
        private const string fileName = "leaderboard.json";
        private string currentPlayerUsername;

        public LocalJsonBackend()
        {
            saveFilePath = Path.Combine(Application.persistentDataPath, fileName);
            LoadLeaderboard();
            Debug.Log($"[LocalJsonBackend] Initialized. Save path: {saveFilePath}");
        }

        public void SavePlayerData(string playerName)
        {
            currentPlayerUsername = playerName;
            Debug.Log($"[LocalJsonBackend] Current player set to: {playerName}");
        }

        public void SaveScore(string playerName, int score)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                Debug.LogWarning("[LocalJsonBackend] Cannot save score without player name");
                return;
            }

            /*
            
            var existingPlayer = leaderboard.players.FirstOrDefault(p => p.username == playerName);

            if (existingPlayer != null)
            {
                if (score > existingPlayer.highScore)
                {
                    Debug.Log($"[LocalJsonBackend] Updated {playerName}: {existingPlayer.highScore} → {score}");
                    existingPlayer.highScore = score;
                }
                else
                {
                    Debug.Log($"[LocalJsonBackend] Score {score} not higher than existing {existingPlayer.highScore} for {playerName}");
                    return; // Don't save if not a high score
                }
            }
            else
            {
                Debug.Log($"[LocalJsonBackend] Added new player {playerName} with score {score}");
                leaderboard.players.Add(new PlayerData(playerName, score));
            }
            
            */
            
            SaveLeaderboard();
        }

        public List<PlayerData> GetLeaderboard()
        {
            //return leaderboard.players.OrderByDescending(p => p.highScore).ToList();
            return null;
        }

        private void LoadLeaderboard()
        {
            /*
            
            if (File.Exists(saveFilePath))
            {
                try
                {
                    var json = File.ReadAllText(saveFilePath);
                    leaderboard = JsonUtility.FromJson<PlayerLeaderboard>(json);
                    Debug.Log($"[LocalJsonBackend] Loaded {leaderboard.players.Count} players from leaderboard");
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"[LocalJsonBackend] Failed to load leaderboard: {e.Message}");
                    leaderboard = new PlayerLeaderboard();
                }
            }
            else
            {
                leaderboard = new PlayerLeaderboard();
                Debug.Log("[LocalJsonBackend] No existing leaderboard found, created new one");
            }
            
            */
        }

        private void SaveLeaderboard()
        {
            /*
            
            try
            {
                leaderboard.players = leaderboard.players.OrderByDescending(p => p.highScore).ToList();
                var json = JsonUtility.ToJson(leaderboard, true);
                File.WriteAllText(saveFilePath, json);
                Debug.Log($"[LocalJsonBackend] Saved leaderboard with {leaderboard.players.Count} players");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[LocalJsonBackend] Failed to save leaderboard: {e.Message}");
            }
            
            */
        }

        public string GetCurrentPlayerUsername()
        {
            return currentPlayerUsername;
        }
    }
}