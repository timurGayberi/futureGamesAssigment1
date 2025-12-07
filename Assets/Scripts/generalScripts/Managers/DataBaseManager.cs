using System.Collections.Generic;
using UnityEngine;
using generalScripts.Interfaces;
using scriptableObjects;

namespace generalScripts.Managers
{
    public class DatabaseManager : MonoBehaviour, IDataManager
    {
        private IDataBackend _currentBackend;
        private PlayFabBackend _onlineBackend;
        private LocalJsonBackend _offlineBackend;

        public string CurrentPlayerUsername { get; private set; }
        public bool IsOnline { get; private set; }

        private void Awake()
        {
            _offlineBackend = new LocalJsonBackend();
            _onlineBackend = new PlayFabBackend();

            ServiceLocator.RegisterService<IDataManager>(this);
            Debug.Log("[DatabaseManager] Registered with ServiceLocator");
            
            _currentBackend = _offlineBackend;
            IsOnline = false;
        }

        private void Start()
        {
            TryConnectPlayFab();
        }

        private void OnDestroy()
        {
            ServiceLocator.UnregisterService<IDataManager>(this);
            Debug.Log("[DatabaseManager] Unregistered from ServiceLocator");
        }
        
        private void TryConnectPlayFab()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.Log("[DatabaseManager] No internet connection - staying offline");
                return;
            }

            Debug.Log("[DatabaseManager] Internet detected - attempting PlayFab login...");

            try
            {
                _onlineBackend.LoginWithDeviceId((success) =>
                {
                    if (success)
                    {
                        Debug.Log("[DatabaseManager] ✓ PlayFab connected - switching to online mode");
                        _currentBackend = _onlineBackend;
                        IsOnline = true;
                    }
                    else
                    {
                        Debug.Log("[DatabaseManager] PlayFab login failed - staying offline (this is normal)");
                        _currentBackend = _offlineBackend;
                        IsOnline = false;
                    }
                });
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[DatabaseManager] Exception during PlayFab connection: {ex.Message}");
                Debug.Log("[DatabaseManager] Falling back to offline mode");
                _currentBackend = _offlineBackend;
                IsOnline = false;
            }
        }

        public void SetCurrentPlayer(string username)
        {
            CurrentPlayerUsername = username;
            _currentBackend.SavePlayerData(username);
            Debug.Log($"[DatabaseManager] Current player set to: {username} (Mode: {(IsOnline ? "Online" : "Offline")})");
        }

        public void LoadLeaderboard()
        {
            Debug.Log("[DatabaseManager] Leaderboard data ready");
        }

        public void AddOrUpdateHighScore(string username, int score)
        {
            _currentBackend.SaveScore(username, score);
            Debug.Log($"[DatabaseManager] Score saved: {username} = {score} (Mode: {(IsOnline ? "Online" : "Offline")})");
        }

        public List<PlayerData> GetSortedLeaderboard()
        {
            return _currentBackend.GetLeaderboard();
        }
        
        public void ForceOfflineMode()
        {
            Debug.Log("[DatabaseManager] Forcing offline mode");
            _currentBackend = _offlineBackend;
            IsOnline = false;
        }
        
        public void RetryOnlineConnection()
        {
            Debug.Log("[DatabaseManager] Retrying PlayFab connection...");
            TryConnectPlayFab();
        }
    }
}
