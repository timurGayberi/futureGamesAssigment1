using UnityEngine;
using MainCharacterScripts;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace generalScripts
{
    public class GameSceneManager : MonoBehaviour
    {
        // A reference to the UI manager for this scene.
        [SerializeField]
        private GameUIManager gameUIManager;
        
        // Reference to the Input Actions asset.
        [SerializeField]
        private GameInput _gameInput;

        private void Awake()
        {
            // Initialize the new Input System.
            _gameInput = new GameInput();
            _gameInput.Gameplay.Pause.performed += ctx => TogglePause();
        }

        private void OnEnable()
        {
            _gameInput.Gameplay.Enable();
        }

        private void OnDisable()
        {
            _gameInput.Gameplay.Disable();
        }

        private void Start()
        {
            // Un-pause the game at the start.
            UnpauseGame();

            // Pass the player's name to the UI manager to display.
            if (JsonSaveManager.Instance != null && !string.IsNullOrEmpty(JsonSaveManager.Instance.CurrentPlayerUsername))
            {
                gameUIManager.SetPlayerName(JsonSaveManager.Instance.CurrentPlayerUsername);
            }
        }

        /// <summary>
        /// This method is called by your game logic to update the score.
        /// It then tells the UI manager to display the new score.
        /// </summary>
        public void UpdateScore(int newScore)
        {
            gameUIManager.UpdateScore(newScore);
        }
        
        /// <summary>
        /// Pauses or unpauses the game and tells the UI manager to toggle the menu.
        /// This is called when the Escape key is pressed.
        /// </summary>
        private void TogglePause()
        {
            if (Time.timeScale == 1f)
            {
                // Pause the game.
                Time.timeScale = 0f;
                gameUIManager.ShowPauseMenu();
            }
            else
            {
                // Un-pause the game.
                Time.timeScale = 1f;
                gameUIManager.HidePauseMenu();
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
    }
}