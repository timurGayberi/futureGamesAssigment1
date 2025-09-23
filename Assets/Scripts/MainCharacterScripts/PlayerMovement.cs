using UnityEngine;

namespace MainCharacterScripts
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerStats _stats;
        private Vector2 _currentMoveInput;
        private Rigidbody2D _rb;
        
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
        public void Tick()
        {
            if (_rb != null)
            {
                _rb.linearVelocity = _currentMoveInput * _stats.moveSpeed;
            }
        }
    }
}