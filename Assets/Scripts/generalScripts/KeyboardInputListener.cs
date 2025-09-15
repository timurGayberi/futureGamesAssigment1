using UnityEngine;

namespace GeneralScripts
{
    public class KeyboardInputListener : MonoBehaviour
    {
        public static KeyboardInputListener Instance;

        public Vector2 MoveInput { get; private set; }
        public float StrafeInput { get; private set; } 
        public float RotationInput { get; private set; }
        public bool MouseLeftClick { get; private set; }
        public bool MouseRightClick { get; private set; }

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
            
            MouseLeftClick = Input.GetMouseButton(0);
            MouseRightClick = Input.GetMouseButton(1);
            
        }
        
    }
}