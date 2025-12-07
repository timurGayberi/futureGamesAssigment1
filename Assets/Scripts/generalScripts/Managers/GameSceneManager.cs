using UnityEngine;

namespace generalScripts.Managers
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
    }
}