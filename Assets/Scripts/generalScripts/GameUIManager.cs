using UnityEngine;
using UnityEngine.SceneManagement;
using MainCharacterScripts;
using TMPro;

namespace generalScripts
{
    public class GameUIManager : MonoBehaviour
    {
        [Header("UI Panels")]
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

        private void Start()
        {
            _currentScore = 0;
            UpdateScore(_currentScore);
            HideGameOverMenu();
        }

        public void ShowPauseMenu()
        {
            pauseMenuPanel.SetActive(true);
        }

        public void HidePauseMenu()
        {
            pauseMenuPanel.SetActive(false);
        }

        public void ShowGameOverMenu()
        {
            HidePauseMenu();
            gameOverPanel.SetActive(true);
            gameOverScoreText.text = "Final Score: " + _currentScore;
        }

        public void HideGameOverMenu()
        {
            gameOverPanel.SetActive(false);
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
            Time.timeScale = 1f;
            HidePauseMenu();
        }

        public void OnMainMenuClicked()
        {
            JsonSaveManager.Instance.AddOrUpdateHighScore(JsonSaveManager.Instance.CurrentPlayerUsername, _currentScore);
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }

        public void OnExitGameClicked()
        {
            JsonSaveManager.Instance.AddOrUpdateHighScore(JsonSaveManager.Instance.CurrentPlayerUsername, _currentScore);
            Application.Quit();
        }
        
        public void OnRestartGameClicked()
        {
            JsonSaveManager.Instance.AddOrUpdateHighScore(JsonSaveManager.Instance.CurrentPlayerUsername, _currentScore);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }
    }
}
