using MainCharacterScripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace generalScripts
{
    public class GameUIManager : MonoBehaviour
    {
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
        
        [Header("HUD")]
        [SerializeField]
        private TextMeshProUGUI healthText;
        [SerializeField]
        private TextMeshProUGUI timerText;
        [SerializeField]
        private TextMeshProUGUI currentScoreText;

        private int _currentScore;
        
        public void InitializeUI()
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
            
            currentScoreText.text = $"{_currentScore}";
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
        
        public void UpdateGameOverScore(int score)
        {
            if (gameOverPanel != null)
            {
                gameOverScoreText.text = "Score: " + score;
            }
        }
        
        public void UpdateHealth(int health)
        {
            if (healthText != null)
            {
                healthText.text = "Health: " + health.ToString();
            }
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
            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RestartGame();
            }
        }
    }
}