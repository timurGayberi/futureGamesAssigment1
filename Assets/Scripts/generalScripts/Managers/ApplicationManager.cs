using generalScripts.Interfaces;
using UnityEngine;

namespace generalScripts.Managers
{
    public class ApplicationManager : MonoBehaviour, IApplicationManager
    {
        public GameState CurrentGameState { get; private set; }

        private void Awake()
        {
            ServiceLocator.RegisterService<IApplicationManager>(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.UnregisterService<IApplicationManager>(this);
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

        public void RestartGame()
        {
            // TODO: Use SceneLoader service once implemented
            // TODO: Reset GameplayManager state
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

            SetGameState(GameState.Gameplay);
            Time.timeScale = 1f;
        }

        public void LoadMainMenu()
        {
            // TODO: Use SceneLoader service once implemented
            Time.timeScale = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            SetGameState(GameState.Gameplay);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void SetGameState(GameState newState)
        {
            CurrentGameState = newState;

            switch (CurrentGameState)
            {
                case GameState.Gameplay:
                    Time.timeScale = 1;
                    break;
                case GameState.Paused:
                    Time.timeScale = 0;
                    break;
                case GameState.GameOver:
                    Time.timeScale = 0;
                    break;
            }
        }

        public void SetGameOverState()
        {
            SetGameState(GameState.GameOver);
        }

        public void SetGameplayState()
        {
            SetGameState(GameState.Gameplay);
        }
    }
}
