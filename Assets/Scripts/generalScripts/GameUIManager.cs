using UnityEngine;
using TMPro;
using MainCharacterScripts;
using UnityEngine.SceneManagement;

namespace generalScripts
{
    public class GameUIManager : MonoBehaviour
    {
// A reference to the Pause Menu UI Panel.
        [SerializeField]
        private GameObject pauseMenuPanel;

        // UI Text elements to display player name and score.
        [SerializeField]
        private TextMeshProUGUI playerNameText;
        [SerializeField]
        private TextMeshProUGUI playerScoreText;

        private int _currentScore;

        /// <summary>
        /// Called by the GameSceneManager to show the pause menu.
        /// </summary>
        public void ShowPauseMenu()
        {
            pauseMenuPanel.SetActive(true);
        }

        /// <summary>
        /// Called by the GameSceneManager to hide the pause menu.
        /// </summary>
        public void HidePauseMenu()
        {
            pauseMenuPanel.SetActive(false);
        }

        /// <summary>
        /// Updates the player's name on the UI.
        /// </summary>
        public void SetPlayerName(string name)
        {
            playerNameText.text = "Player: " + name;
        }

        /// <summary>
        /// Updates the score on the UI.
        /// </summary>
        public void UpdateScore(int score)
        {
            _currentScore = score;
            playerScoreText.text = "Score: " + _currentScore;
        }

        // --- Button Actions for the Pause Menu ---

        public void OnContinueClicked()
        {
            // Un-pause the game. The GameSceneManager will handle this.
            Time.timeScale = 1f;
            HidePauseMenu();
        }

        public void OnMainMenuClicked()
        {
            // First, save the current player's score.
            JsonSaveManager.Instance.AddOrUpdateHighScore(JsonSaveManager.Instance.CurrentPlayerUsername, _currentScore);

            // Then, load the main menu scene.
            Time.timeScale = 1f; // Always unpause before loading a new scene.
            SceneManager.LoadScene(0); // Assuming Main Menu is at index 0.
        }

        public void OnExitGameClicked()
        {
            // First, save the current player's score.
            JsonSaveManager.Instance.AddOrUpdateHighScore(JsonSaveManager.Instance.CurrentPlayerUsername, _currentScore);

            // Then, exit the game.
            Application.Quit();
        }
    }
}
