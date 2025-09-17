using generalScripts;
using UnityEngine;
using generalScripts;

namespace MainCharacterScripts
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private Health health;

        private void Awake(){}

        public void Die()
        {
            Debug.Log("Player has died");
            Destroy(gameObject);
        }

    }
}
