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

        public float ThrottleInput { get; private set; }
        public float SteerInput { get; private set; }
        public Vector2 MouseWorldPosition { get; private set; }
        public float DistanceToMouse { get; private set; }
        public bool CanMove { get; private set; }
        public bool CanRotate { get; private set; }
        public float TargetRotationAngle { get; private set; }
        public bool ShouldRotate { get; private set; }
        public bool IsBoosting { get; private set; }

        private void Start()
        {
            inputManager = ServiceLocator.GetService<IInputManager>();
            if (inputManager == null)
            {
                enabled = false;
                return;
            }

            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                enabled = false;
            }
        }

        private void Update()
        {
            ProcessInputs();
        }

        private void ProcessInputs()
        {
            ThrottleInput = inputManager.ThrottleInput;
            SteerInput = inputManager.SteerInput;
            var aimPos = inputManager.AimPosition;
            IsBoosting = inputManager.IsBoosting;

            MouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(aimPos.x, aimPos.y, 10f));
            var directionToMouse = MouseWorldPosition - (Vector2)transform.position;
            DistanceToMouse = directionToMouse.magnitude;

            CanMove = true;

            CanRotate = DistanceToMouse > rotationDeadZone;

            if (CanRotate)
            {
                var direction = directionToMouse.normalized;
                TargetRotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            }

            var currentAngle = transform.eulerAngles.z;
            var angleDifference = Mathf.DeltaAngle(currentAngle, TargetRotationAngle);

            ShouldRotate = Mathf.Abs(angleDifference) > angleThreshold;
        }

        public float GetRotationSmoothFactor()
        {
            return rotationSmoothFactor;
        }

        /*
        public bool IsMovementBlocked()
        {
            return ThrottleInput != 0 && !CanMove;
        }
        */
    }
}
