using UnityEngine;

namespace GeneralScripts
{
    public class KeyboardInputListener : MonoBehaviour
    {
        public static KeyboardInputListener Instance { get; private set; }

        public Vector2 MoveInput { get; private set; }
        public float StrafeInput { get; private set; } // New property for strafing (Q/E)
        public float RotationInput { get; private set; } // New property for rotation (A/D)

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            // Get forward/backward input from W/S keys.
            float y = Input.GetAxisRaw("Vertical");
            MoveInput = new Vector2(0, y);
            
            StrafeInput = 0f;
            if (Input.GetKey(KeyCode.D))
            {
                StrafeInput = 1f;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                StrafeInput = -1f;
            }
            
            RotationInput = 0f;
            if (Input.GetKey(KeyCode.E))
            {
                RotationInput = -1f;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                RotationInput = 1f;
            }
        }
    }
}