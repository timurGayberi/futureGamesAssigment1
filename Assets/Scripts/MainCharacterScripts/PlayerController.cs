using UnityEngine;
using GeneralScripts; // Now we reference our new input listener.

namespace MainCharacterScripts
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Component References")]
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerShooting playerShooting;

        [Header("Stats")]
        [SerializeField] private PlayerStats playerStats;
        
        private void Awake()
        {
            playerMovement.Initialize(playerStats);
        }

        private void Update()
        {
            var moveInput = KeyboardInputListener.Instance.MoveInput;
            float strafeInput = KeyboardInputListener.Instance.StrafeInput;
            float rotationInput = KeyboardInputListener.Instance.RotationInput;
            
            Vector2 relativeMovement = (Vector2)transform.up * moveInput.y + (Vector2)transform.right * strafeInput;
            
            if (relativeMovement.magnitude > 1f)
            {
                relativeMovement.Normalize();
            }
            
            playerMovement.SetInput(relativeMovement);
            
            playerMovement.Tick();
            
            transform.Rotate(Vector3.forward, rotationInput * playerStats.rotationSpeed * Time.deltaTime);
        }
    }
}
