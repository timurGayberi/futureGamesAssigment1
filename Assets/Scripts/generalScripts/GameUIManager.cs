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
            // Set the initial score text to zero when the game starts.
            UpdateScore(_currentScore);
            ShowGameplayUI();
            
            // Set the player's name from the JsonSaveManager.
            if (JsonSaveManager.Instance != null && !string.IsNullOrEmpty(JsonSaveManager.Instance.CurrentPlayerUsername))
            {
                SetPlayerName(JsonSaveManager.Instance.CurrentPlayerUsername);
            }
        }
        
        /// <summary>
        /// Shows the main gameplay UI and hides all other panels.
        /// </summary>
        public void ShowGameplayUI()
        {
            gameplayPanel.SetActive(true);
            pauseMenuPanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }

        /// <summary>
        /// Shows the pause menu and hides the gameplay UI.
        /// </summary>
        public void ShowPauseMenu()
        {
            pauseMenuPanel.SetActive(true);
            gameplayPanel.SetActive(false);
        }

        /// <summary>
        /// Shows the game over menu and hides the gameplay UI.
        /// </summary>
        public void ShowGameOverMenu()
        {
            gameOverPanel.SetActive(true);
            gameplayPanel.SetActive(false);
            // Display the final score on the game over panel.
            //gameOverScoreText.text = "Final Score: " + _currentScore;
        }

        public void SetPlayerName(string name)
        {
            playerNameText.text = "Player: " + name;
        }

        public void UpdateScore(int score)
        {
            _currentScore = score;
            // Now you can simply update the text with the new score.
            playerScoreText.text = "Score: " + _currentScore;
        }

        /// <summary>
        /// Handles the logic for the "Continue" button on the pause menu.
        /// </summary>
        public void OnContinueClicked()
        {
            // We ask the GameManager to handle the state change.
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TogglePause();
            }
        }

        /// <summary>
        /// Handles the logic for the "Main Menu" button.
        /// </summary>
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

        /// <summary>
        /// Handles the logic for the "Exit Game" button.
        /// </summary>
        public void OnExitGameClicked()
        {
            if (JsonSaveManager.Instance != null)
            {
                JsonSaveManager.Instance.AddOrUpdateHighScore(JsonSaveManager.Instance.CurrentPlayerUsername, _currentScore);
            }
            Application.Quit();
        }
        
        /// <summary>
        /// Handles the logic for the "Restart" button.
        /// </summary>
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
