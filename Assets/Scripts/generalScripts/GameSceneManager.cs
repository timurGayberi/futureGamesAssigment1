using UnityEngine;
using generalScripts;
using MainCharacterScripts;

namespace generalScripts
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private Transform playerSpawnPoint;

        private void Start()
        {
            if (playerPrefab != null && playerSpawnPoint != null)
            {
                Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.TogglePause();
            }
        }
    }
}