namespace generalScripts.Interfaces
{
    public interface IApplicationManager
    {
        GameState CurrentGameState { get; }

        void TogglePause();
        void RestartGame();
        void LoadMainMenu();
        void QuitGame();
        void SetGameOverState();
        void SetGameplayState();
    }
}