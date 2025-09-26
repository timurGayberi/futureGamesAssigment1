using Collectibles;
using MainCharacterScripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace generalScripts
{
    public class GameUIManager : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField]
        private PlayerStats playerData;
        
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
        [SerializeField] 
        private TextMeshProUGUI killCountText;
        [SerializeField]
        private TextMeshProUGUI levelText;
        [SerializeField]
        private GameObject slider;
        
        private Droplet _droplet;

        private int _currentScore,
                    _killCount;
                    
        private float _currentHealth;            
        
        public void InitializeUI()
        {
            _currentScore = 0;
            _killCount = 0;
            
            UpdateScore(_currentScore);
            UpdateKillCount(_killCount);
            
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
            
            currentScoreText.text = "Current score: " + _currentScore;
            killCountText.text = "Kill count: " + _killCount;
            healthText.text = "Health: " + Mathf.Max(0, _currentHealth).ToString("F0");

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
        
        public void UpdateHealth(float health)
        {
            _currentHealth = health;
            healthText.text = "Health: " + Mathf.Max(0, _currentHealth).ToString("F0"); 
        }

        private void UpdateLevelSlider(){}

        public void UpdateKillCount(int killCount)
        {
            _killCount = killCount;
            killCountText.text = "Kill count: " + _killCount;
        }

        public void UpdateLevel(int value)
        {
            levelText.text = "Level:" + value;
        }
        
        public void UpdateGameOverScore(int score)
        {
            if (gameOverPanel != null)
            {
                gameOverScoreText.text = "Score: " + score;
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