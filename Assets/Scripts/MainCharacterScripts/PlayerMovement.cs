using UnityEngine;

namespace MainCharacterScripts
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerStats _stats;
        private Vector2 _currentMoveInput;
        private Rigidbody2D _rb;

        private bool _isBoosting; // Add this field

        public void Initialize(PlayerStats stats)
        {
            _stats = stats;
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0;
        }

        public void SetInput(Vector2 moveInput)
        {
            _currentMoveInput = moveInput;
        }

        public void SetBoosting(bool isBoosting)
        {
            _isBoosting = isBoosting;
        }

        public void Tick()
        {
            if (_rb != null)
            {
                float currentSpeed = _isBoosting ? _stats.boostSpeed : _stats.moveSpeed;
                _rb.linearVelocity = _currentMoveInput * currentSpeed;
            }
        }
    }
}