using UnityEngine;

namespace GeneralScripts
{
    public class KeyboardInputListener : MonoBehaviour
    {
        public static KeyboardInputListener Instance { get; private set; }
        
        public Vector2 MoveInput { get; private set; }
        public float StrafeInput { get; private set; }
        public float RotationInput { get; private set; }
        public bool MouseLeftClick { get; private set; }
        public bool MouseRightClick { get; private set; }
        public bool IsEscapePressed { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            MoveInput = Vector2.zero;
            StrafeInput = 0f;
            RotationInput = 0f;
            IsEscapePressed = false;
            
            float verticalInput = Input.GetAxisRaw("Vertical");
            MoveInput = new Vector2(0, verticalInput);
            
            float strafeInput = 0f;
            if (Input.GetKey(KeyCode.D))
            {
                strafeInput = 1f;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                strafeInput = -1f;
            }
            StrafeInput = strafeInput;
            
            float rotationInput = 0f;
            if (Input.GetKey(KeyCode.E))
            {
                rotationInput = -1f;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                rotationInput = 1f;
            }
            RotationInput = rotationInput;
            
            MouseLeftClick = Input.GetMouseButton(0);
            MouseRightClick = Input.GetMouseButtonDown(1);
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                IsEscapePressed = true;
            }
        }
    }
}
