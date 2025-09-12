using System;
using UnityEngine;
using TMPro;
using MainCharacterScripts;
using UnityEngine.SceneManagement;

namespace generalScripts
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject pauseMenuPanel;
        
        [SerializeField]
        private TextMeshProUGUI playerNameText;
        
        [SerializeField]
        private TextMeshProUGUI playerScoreText;
        
        private string _playerNoName;

        private int _currentScore;

        private void Start()
        {
            _currentScore = 0;
            UpdateScore(_currentScore);
            
            _playerNoName = "no name";
            
            if (playerNameText == null)
            {
                SetPlayerName(_playerNoName);
            }
            
        }

        public void ShowPauseMenu()
        {
            pauseMenuPanel.SetActive(true);
        }
        
        public void HidePauseMenu()
        {
            pauseMenuPanel.SetActive(false);
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
    }
}
