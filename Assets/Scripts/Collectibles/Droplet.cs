using MainCharacterScripts;
using UnityEngine;

namespace Collectibles
{
    public enum CollectibleType
    {
        Experience,
        HealthPoints,
        DamageBoost,
        SpeedBoost,
        Shield,
        Missile
    }
    public class Droplet : MonoBehaviour
    {
        public int dropletValue = 1;
        
        private PlayerLevelController _playerLevelController;

        public void Awake()
        {
            DontDestroyOnLoad(this);
            
            _playerLevelController = GameObject.Find ("PlayerPrototype").GetComponent<PlayerLevelController> ();
            
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Collected(dropletValue);
            }
        }

        private void Collected(int value)
        {
            if (_playerLevelController != null)
            {
                _playerLevelController.UpdateLevel(value);
                Debug.Log("Collected " + value);
                Destroy();
            }
            
        }
        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
