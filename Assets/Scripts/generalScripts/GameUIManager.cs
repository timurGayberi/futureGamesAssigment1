using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using MainCharacterScripts;

namespace generalScripts
{
    // Make this a singleton so it can be easily accessed from other scripts.
    public class GameUIManager : MonoBehaviour
    {
        public static GameUIManager Instance { get; private set; }

        [Header("UI Panels")]
        [SerializeField]
        private GameObject gameplayPanel;
        [SerializeField]
        private GameObject pauseMenuPanel;
        [SerializeField]
        private GameObject gameOverPanel;

        [Header("UI Text")]
        [SerializeField]
        private TextMeshProUGUI playerNameText;
        [SerializeField]
        private TextMeshProUGUI playerScoreText;
        [SerializeField]
        private TextMeshProUGUI gameOverScoreText;
        [SerializeField]
        private TextMeshProUGUI timerText;

        private int _currentScore;

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

        private void Start()
        {
            _currentScore = 0;
            UpdateScore(_currentScore);
            ShowGameplayUI();
            
            if (JsonSaveManager.Instance != null && !string.IsNullOrEmpty(JsonSaveManager.Instance.CurrentPlayerUsername))
            {
                SetPlayerName(JsonSaveManager.Instance.CurrentPlayerUsername);
            }
        }

        public void Update()
        {
            if (GameManager.Instance != null && timerText != null)
            {
                timerText.text = GameManager.Instance.GetFormatedTime();
            }
        }

        public void ShowGameplayUI()
        {
            gameplayPanel.SetActive(true);
            pauseMenuPanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }
        
        public void ShowPauseMenu()
        {
            pauseMenuPanel.SetActive(true);
            gameplayPanel.SetActive(false);
        }
        
        public void ShowGameOverMenu()
        {
            gameOverPanel.SetActive(true);
            gameplayPanel.SetActive(false);
        }

        public void SetPlayerName(string name)
        {
            playerNameText.text = "Player: " + name;
        }

        public void UpdateScore(int score)
        {
            _currentScore = score;
            playerScoreText.text = "Score: " + _currentScore;
        }
        
        public void OnContinueClicked()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TogglePause();
            }
        }
        
        public void OnMainMenuClicked()
        {
            if (JsonSaveManager.Instance != null)
            {
                JsonSaveManager.Instance.AddOrUpdateHighScore(JsonSaveManager.Instance.CurrentPlayerUsername, _currentScore);
            }
            // Reset time scale to ensure the main menu is not paused.
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
        
        public void OnExitGameClicked()
        {
            if (JsonSaveManager.Instance != null)
            {
                JsonSaveManager.Instance.AddOrUpdateHighScore(JsonSaveManager.Instance.CurrentPlayerUsername, _currentScore);
            }
            Application.Quit();
        }

        public void OnRestartGameClicked()
        {
            if (JsonSaveManager.Instance != null)
            {
                JsonSaveManager.Instance.AddOrUpdateHighScore(JsonSaveManager.Instance.CurrentPlayerUsername, _currentScore);
            }
            // Ask the GameManager to handle the restart.
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RestartGame();
            }
        }
    }
}
