using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using scriptableObjects;

namespace MainCharacterScripts
{
    public class JsonSaveManager : MonoBehaviour
{
    public static JsonSaveManager Instance { get; private set; }
    
    public string CurrentPlayerUsername { get; private set; }
    
    private PlayerLeaderboard leaderboard = new PlayerLeaderboard();
    private string saveFilePath;
    private const string fileName = "leaderboard.json";

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
        
        saveFilePath = Path.Combine(Application.persistentDataPath, fileName);
    }
    
    private void Start()
    {
        LoadLeaderboard();
    }
    
    public void SetCurrentPlayer(string username)
    {
        CurrentPlayerUsername = username;
    }
    
    public void LoadLeaderboard()
    {
        if (File.Exists(saveFilePath))
        {
            var json = File.ReadAllText(saveFilePath);
            leaderboard = JsonUtility.FromJson<PlayerLeaderboard>(json);
        }
        else
        {
            leaderboard = new PlayerLeaderboard();
        }
    }
    
    private void SaveLeaderboard()
    {
        leaderboard.players = leaderboard.players.OrderByDescending(p => p.highScore).ToList();
        
        var json = JsonUtility.ToJson(leaderboard, true);
        File.WriteAllText(saveFilePath, json);
    }
    
    public void AddOrUpdateHighScore(string username, int newScore)
    {
        var existingPlayer = leaderboard.players.FirstOrDefault(p => p.username == username);

        if (existingPlayer != null)
        {
            if (newScore > existingPlayer.highScore)
            {
                existingPlayer.highScore = newScore;
            }
        }
        else
        {
            leaderboard.players.Add(new PlayerData(username, newScore));
        }
        SaveLeaderboard();
    }
    

    public List<PlayerData> GetSortedLeaderboard()
    {
        return leaderboard.players.OrderByDescending(p => p.highScore).ToList();
    }
}
}