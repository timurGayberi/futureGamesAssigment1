using System.Collections.Generic;
using generalScripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace generalScripts.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject mainMenu;

        [SerializeField]
        private TMP_InputField nameInputField;

        [SerializeField]
        private List<GameObject> panels = new List<GameObject>();

        private enum CurrentPanel
        {
            MainMenu = 0,
            StartGame = 1,
            Extras = 2,
            Exit = 3
        }

        private CurrentPanel _currentPanel;

        private void Start()
        {
            _currentPanel = CurrentPanel.MainMenu;
            UpdateUI();
        }

        private void UpdateUI()
        {
            foreach (var panel in panels)
            {
                panel.SetActive(false);
            }

            panels[(int)_currentPanel].SetActive(true);
        }

        public void OnStartButtonClick()
        {
            Debug.Log("OnStartButtonClick");
            _currentPanel = CurrentPanel.StartGame;
            UpdateUI();
        }

        public void OnExtrasButtonClick()
        {
            Debug.Log("OnExtrasButtonClick");
            _currentPanel = CurrentPanel.Extras;
            UpdateUI();
        }

        public void OnExitButtonClick()
        {
            Debug.Log("OnExitButtonClick");
            _currentPanel = CurrentPanel.Exit;
            UpdateUI();
        }

        public void OnBackButtonClick()
        {
            Debug.Log("Go back button clicked");
            _currentPanel = CurrentPanel.MainMenu;
            UpdateUI();
        }

        public void OnConfirmExitClicked()
        {
            Application.Quit();
        }

        public void OnStartGameConfirmed()
        {
            var playerName = nameInputField.text;

            if (string.IsNullOrEmpty(playerName))
            {
                Debug.LogWarning("Player name is empty");
                return;
            }

            var dataManager = ServiceLocator.GetService<IDataManager>();
            dataManager.SetCurrentPlayer(playerName);

            Debug.Log($"[MainMenu] Starting game for player: {playerName}");
            SceneManager.LoadScene((int)SceneIndex.GameScene);
        }
    }
}
