using UnityEngine;
using generalScripts;

namespace MainCharacterScripts
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private Health health;

        private void Awake()
        {
            if (health != null)
            {
                health.onDie.AddListener(Die);
            }
        }

        private void OnDestroy()
        {
            if (health != null)
            {
                health.onDie.RemoveListener(Die);
            }
        }

        public void Die()
        {
            //Debug.Log("Player has died!");
            GameManager.Instance.OnPlayerDied();
            Destroy(gameObject);
        }
    }
}