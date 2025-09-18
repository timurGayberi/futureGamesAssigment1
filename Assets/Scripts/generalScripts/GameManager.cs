using MainCharacterScripts;
using proceduralMaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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
        
        private void Start()
        {
            _gameUIManager = GameUIManager.Instance;
            _gameTime = Time.realtimeSinceStartup;
            SetGameState(GameState.Gameplay);
        }

        private void Update()
        {
            if (CurrentGameState == GameState.Gameplay)
            {
                _gameTime += Time.deltaTime;
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void RegisterUIManager(GameUIManager uiManager)
        {
            _gameUIManager = uiManager;
        }

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

        public void OnPlayerDied()
        {
            SetGameState(GameState.GameOver);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void SetGameState(GameState newState)
        {
            CurrentGameState = newState;

            switch (CurrentGameState)
            {
                case GameState.Gameplay:
                    Time.timeScale = 1;
                    if (_gameUIManager != null)
                    {
                        _gameUIManager.ShowGameplayUI();
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
                    }
                    break;
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == SceneManager.GetActiveScene().buildIndex)
            {
                if (WorldManager.Instance != null)
                {
                    PlayerController newPlayer = FindObjectOfType<PlayerController>();
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
                seconds = Mathf.FloorToInt(_gameTime % 60);
            
            return $"{hours:00}:{minutes:00}:{seconds:00}";
        }
    }
}
