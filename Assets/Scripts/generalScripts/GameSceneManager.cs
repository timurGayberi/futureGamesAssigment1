using UnityEngine;
using UnityEngine.SceneManagement;
using MainCharacterScripts;

namespace generalScripts
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField] private GameUIManager gameUIManager;

        private void Start()
        {
            UnpauseGame();

            if (JsonSaveManager.Instance != null && !string.IsNullOrEmpty(JsonSaveManager.Instance.CurrentPlayerUsername))
            {
                gameUIManager.SetPlayerName(JsonSaveManager.Instance.CurrentPlayerUsername);
            }
        }

        private void Update()
        {
            // Listen for ESC key press using old input system
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        public void UpdateScore(int newScore)
        {
            gameUIManager.UpdateScore(newScore);
        }

        private void TogglePause()
        {
            if (Time.timeScale == 1f)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }

        private void PauseGame()
        {
            Time.timeScale = 0f;
            gameUIManager.ShowPauseMenu();
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1f;
            gameUIManager.HidePauseMenu();
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}