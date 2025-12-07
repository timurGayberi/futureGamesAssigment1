using generalScripts;
using UnityEngine;
using generalScripts.Interfaces;

namespace MainCharacterScripts
{
    public class PlayerInputProcessor : MonoBehaviour
    {
        [Header("Dead Zone Settings")]
        [SerializeField] private float movementDeadZone = 2.0f;
        [SerializeField] private float rotationDeadZone = 1.0f;
        [SerializeField] private float angleThreshold = 2.0f;
        
        [Header("Rotation Smoothing")]
        [SerializeField] private float rotationSmoothFactor = 0.15f;
        
        private IInputManager inputManager;
        private Camera mainCamera;
        
        // Processed Input Values (read-only for other classes)
        public float ThrottleInput { get; private set; }
        public float SteerInput { get; private set; }
        public Vector2 MouseWorldPosition { get; private set; }
        public float DistanceToMouse { get; private set; }
        public bool CanMove { get; private set; }
        public bool CanRotate { get; private set; }
        public float TargetRotationAngle { get; private set; }
        public bool ShouldRotate { get; private set; }
        
        private void Start()
        {
            inputManager = ServiceLocator.GetService<IInputManager>();
            if (inputManager == null)
            {
                Debug.LogError("[PlayerInputProcessor] InputManager not found!");
                enabled = false;
                return;
            }
            
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("[PlayerInputProcessor] Main Camera not found!");
                enabled = false;
            }
        }
        
        private void Update()
        {
            ProcessInputs();
        }
        
        private void ProcessInputs()
        {
            // Read raw inputs
            ThrottleInput = inputManager.ThrottleInput;
            SteerInput = inputManager.SteerInput;
            Vector2 aimPos = inputManager.AimPosition;
            
            // Calculate mouse world position
            MouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(aimPos.x, aimPos.y, 10f));
            Vector2 directionToMouse = MouseWorldPosition - (Vector2)transform.position;
            DistanceToMouse = directionToMouse.magnitude;
            
            // Apply Movement Dead Zone
            CanMove = DistanceToMouse > movementDeadZone;
            
            // Apply Rotation Dead Zone
            CanRotate = DistanceToMouse > rotationDeadZone;
            
            // Calculate target rotation angle
            if (CanRotate)
            {
                Vector2 direction = directionToMouse.normalized;
                TargetRotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                
                // Check if rotation is needed (angular threshold)
                float currentAngle = transform.eulerAngles.z;
                float angleDifference = Mathf.DeltaAngle(currentAngle, TargetRotationAngle);
                
                ShouldRotate = Mathf.Abs(angleDifference) > angleThreshold;
            }
            else
            {
                ShouldRotate = false;
            }
        }
        
        /// <summary>
        /// Get rotation smooth factor for Lerp
        /// </summary>
        public float GetRotationSmoothFactor()
        {
            return rotationSmoothFactor;
        }
        
        /// <summary>
        /// Check if player is trying to move but blocked by dead zone
        /// </summary>
        public bool IsMovementBlocked()
        {
            return ThrottleInput != 0 && !CanMove;
        }
    }
}
