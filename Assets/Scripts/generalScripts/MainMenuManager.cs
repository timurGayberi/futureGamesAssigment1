using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using MainCharacterScripts;

namespace generalScripts
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
            Exit =3
        }

        private CurrentPanel _currentPanel;

        private void Start()
        {
            _currentPanel = CurrentPanel.MainMenu;
            UpdateUI();
        }

        private void UpdateUI()
        {
            foreach (GameObject panel in panels)
            {
                panel.SetActive(false);
            }
            
            panels[(int)_currentPanel].SetActive(true);

            /*switch (_currentPanel)
            {
                case CurrentPanel.MainMenu:
                    mainMenu.SetActive(true);
                    break;
                case CurrentPanel.StartGame:
                    mainMenu.SetActive(false);
                    break;
                case CurrentPanel.Extras:
                    mainMenu.SetActive(false);
                    _currentPanel = CurrentPanel.Extras;
                    break;
                case CurrentPanel.Exit:
                    mainMenu.SetActive(false);
                    break;
                default:
                    Debug.LogError("Unknown CurrentPanel");
                    break;
            }*/
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
            string playerName = nameInputField.text;

            if (string.IsNullOrEmpty(playerName))
            {
                Debug.LogWarning("Player name is empty");
                return;
            }
            
            JsonSaveManager.Instance.SetCurrentPlayer(playerName);
            
            SceneManager.LoadScene(1);
        }
    }
}
