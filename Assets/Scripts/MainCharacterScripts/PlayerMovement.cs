using UnityEngine;

namespace MainCharacterScripts
{
    /// <summary>
    /// This script handles the physical movement of the player character.
    /// It receives input from the PlayerController and applies it to the Rigidbody.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        // Private fields for storing data and component references.
        private PlayerStats _stats;
        private Vector2 _currentMoveInput;
        private Rigidbody2D _rb;

        /// <summary>
        /// Initializes the PlayerMovement script with player stats and caches the Rigidbody2D component.
        /// This is called from the PlayerController's Awake() method.
        /// </summary>
        /// <param name="stats">The stats object containing the player's movement speed.</param>
        public void Initialize(PlayerStats stats)
        {
            _stats = stats;
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0; // Disable gravity for top-down movement.
        }

        /// <summary>
        /// Sets the current movement input vector.
        /// This method is called every frame by the PlayerController.
        /// </summary>
        /// <param name="moveInput">The normalized Vector2 representing the player's movement direction.</param>
        public void SetInput(Vector2 moveInput)
        {
            _currentMoveInput = moveInput;
        }

        /// <summary>
        /// Applies the movement to the player's Rigidbody.
        /// This method is called every frame by the PlayerController.
        /// </summary>
        public void Tick()
        {
            // Set the velocity directly on the Rigidbody to move the player.
            // This method works well for top-down games.
            if (_rb != null)
            {
                _rb.linearVelocity = _currentMoveInput * _stats.moveSpeed;
            }
        }
    }
}