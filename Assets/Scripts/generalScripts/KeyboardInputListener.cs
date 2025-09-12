using UnityEngine;

namespace GeneralScripts
{
    /// <summary>
    /// A singleton class for listening to keyboard inputs.
    /// This allows other scripts to easily access the state of keyboard presses.
    /// </summary>
    public class KeyboardInputListener : MonoBehaviour
    {
        public static KeyboardInputListener Instance { get; private set; }

        // Public properties to be accessed by other classes.
        public Vector2 MoveInput { get; private set; }
        public bool IsEscapePressed { get; private set; }

        private void Awake()
        {
            // Singleton pattern to ensure only one instance exists.
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            // Reset input values at the start of the frame.
            MoveInput = Vector2.zero;
            IsEscapePressed = false;

            // Check for WASD and Arrow key input.
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                MoveInput += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                MoveInput += Vector2.down;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                MoveInput += Vector2.left;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                MoveInput += Vector2.right;
            }
            
            // Normalize the vector to prevent faster diagonal movement.
            if (MoveInput.magnitude > 1)
            {
                MoveInput.Normalize();
            }

            // Check for the Escape key.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                IsEscapePressed = true;
            }
        }
    }
}
