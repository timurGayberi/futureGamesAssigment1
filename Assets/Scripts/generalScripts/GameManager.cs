using UnityEngine;
using UnityEngine.SceneManagement;
using MainCharacterScripts; 
using generalScripts; 

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

        private GameUIManager _gameUIManager;

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

        private void Start()
        {
            // Get the singleton instance of the UI manager.
            _gameUIManager = GameUIManager.Instance;
            SetGameState(GameState.Gameplay);
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
            SetGameState(GameState.Gameplay);
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
    }
}
