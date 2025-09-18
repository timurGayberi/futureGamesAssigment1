using System;
using MainCharacterScripts;
using proceduralMaps;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace generalScripts
{
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState CurrentGameState { get; private set; }
        
        private float _gameTime;
        private int _currentScore;

        private GameUIManager _gameUIManager;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        [Obsolete("Obsolete")]
        private void Start()
        {
            SetGameState(GameState.Gameplay);
        }

        private void Update()
        {
            if (CurrentGameState == GameState.Gameplay)
            {
                _gameTime += Time.deltaTime;
            }
        }

        [Obsolete("Obsolete")]
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void AddScore(int score)
        {
            _currentScore += score;
            if(_gameUIManager != null)
            {
                _gameUIManager.UpdateScore(_currentScore);
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        [Obsolete("Obsolete")]
        public void TogglePause()
        {
            if (CurrentGameState == GameState.Gameplay)
            {
                SetGameState(GameState.Paused);
            }
            else if (CurrentGameState == GameState.Paused)
            {
                SetGameState(GameState.Gameplay);
            }
        }

        [Obsolete("Obsolete")]
        public void OnPlayerDied()
        {
            SetGameState(GameState.GameOver);
        }

        public void RestartGame()
        {
            _gameTime = 0;
            _currentScore = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        [Obsolete("Obsolete")]
        private void SetGameState(GameState newState)
        {
            CurrentGameState = newState;

            if (_gameUIManager == null)
            {
                _gameUIManager = FindObjectOfType<GameUIManager>();
            }
            if (_gameUIManager == null)
            {
                return;
            }

            switch (CurrentGameState)
            {
                case GameState.Gameplay:
                    Time.timeScale = 1;
                    if (_gameUIManager != null)
                    {
                        _gameUIManager.ShowGameplayUI();
                        _gameUIManager.UpdateScore(_currentScore);
                    }
                    break;
                case GameState.Paused:
                    Time.timeScale = 0;
                    if (_gameUIManager != null)
                    {
                        _gameUIManager.ShowPauseMenu();
                    }
                    break;
                case GameState.GameOver:
                    if (_gameUIManager != null)
                    {
                        _gameUIManager.ShowGameOverMenu();
                        _gameUIManager.UpdateGameOverScore(_currentScore);
                    }
                    break;
                
                default:
                    Debug.Log("Game state not set");
                    break;
                
            }
        }

        [Obsolete("Obsolete")]
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == SceneManager.GetActiveScene().buildIndex)
            {
                _gameUIManager = FindObjectOfType<GameUIManager>();
                if (_gameUIManager != null)
                {
                    _gameUIManager.InitializeUI();
                }
                
                if (WorldManager.Instance != null)
                {
                    var newPlayer = FindObjectOfType<PlayerController>();
                    if (newPlayer != null)
                    {
                        WorldManager.Instance.ResetWorld(newPlayer.transform);
                    }
                }
                
                SetGameState(GameState.Gameplay);
            }
        }
        
        public string GetFormatedTime()
        {
            int hours = Mathf.FloorToInt(_gameTime / 3600),
                minutes = Mathf.FloorToInt((_gameTime % 3600) / 60),
                seconds = Mathf.FloorToInt((_gameTime % 3600) % 60);
            
            return $"{hours:00}:{minutes:00}:{seconds:00}";
        }
    }
}
