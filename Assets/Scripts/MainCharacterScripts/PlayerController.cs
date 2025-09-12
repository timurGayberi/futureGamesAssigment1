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

        private const float RotationOffset = -90f;

        private void Awake()
        {
            playerMovement.Initialize(playerStats);
        }

        private void Update()
        {
            var moveInput = KeyboardInputListener.Instance.MoveInput;
            
            Vector2 relativeMovement = transform.up * moveInput.y + transform.right * moveInput.x;
            
            if (relativeMovement.magnitude > 1f)
            {
                relativeMovement.Normalize();
            }
            
            playerMovement.SetInput(relativeMovement);
            
            playerMovement.Tick();
            
            RotateTowardMouse();
        }

        private void RotateTowardMouse()
        {
            Vector3 mousePosition = Input.mousePosition;
            
            mousePosition.z = transform.position.z - Camera.main.transform.position.z;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
            Vector3 direction = mouseWorldPosition - transform.position;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + RotationOffset));
        }
    }
}
